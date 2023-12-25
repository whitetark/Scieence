using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScieenceAPI.Models;
using ScieenceAPI.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ScieenceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(AccountServices accountServices, IConfiguration configuration, FavouriteServices favouriteServices) : ControllerBase
    {
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<Account>> Register([FromBody] UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);

            var refreshToken = GenerateRefreshToken();
            var userRequest = new Account
            {
                Username = request.username,
                PasswordHash = passwordHash,
                RefreshToken = refreshToken.Token,
                TokenCreated = refreshToken.Created,
                TokenExpires = refreshToken.Expires,

            };
            var user = await accountServices.AddAccount(userRequest);
            string token = GenerateAccessToken(user);
            SetResponseCookies(refreshToken, user);
            var result = CreateUserResponse(user, new List<DbPublication>());
            return Ok(new { token, result });
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<object>> Login([FromBody] UserDto request)
        {
            var responseFromDb = await accountServices.GetAccountByUsername(request.username);

            if (responseFromDb == null)
            {
                return BadRequest("User not found");
            }

            var user = responseFromDb.account;
            if (!BCrypt.Net.BCrypt.Verify(request.password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            SetResponseCookies(refreshToken, user);

            var publications = await favouriteServices.GetFavoritesByUsername(request.username);
            var result = CreateUserResponse(responseFromDb.account, publications);
            return Ok(new { token, result });
        }

        [Route("logout")]
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            var username = Request.Cookies["username"];
            if (username == null)
            {
                return BadRequest("User not found");
            }
            var responseFromDb = await accountServices.GetAccountByUsername(username);
            var user = responseFromDb.account;

            user.RefreshToken = "";
            user.TokenExpires = DateTime.UtcNow.ToString("s");
            user.TokenCreated = DateTime.UtcNow.ToString("s");
            _ = accountServices.UpdateAccount(user);

            Response.Cookies.Delete("username");
            Response.Cookies.Delete("refresh_token");

            return Ok();
        }

        [Route("refresh-token")]
        [HttpPost]
        public async Task<ActionResult<object>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refresh_token"];
            var username = Request.Cookies["username"];
            if (refreshToken == null)
            {
                return BadRequest("Refresh Token not found");
            }

            if (username == null)
            {
                return BadRequest("No Username");
            }

            var responseFromDb = await accountServices.GetAccountByUsername(username);
            var user = responseFromDb.account;

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return BadRequest("Wrong refresh token");
            }

            if (DateTime.Parse(user.TokenExpires) < DateTime.Now)
            {
                return BadRequest("Token expired");
            }

            string token = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetResponseCookies(newRefreshToken, user);

            var publications = await favouriteServices.GetFavoritesByUsername(user.Username);
            var result = CreateUserResponse(responseFromDb.account, publications);
            return Ok(new { token, result });
        }

        [Authorize]
        [Route("checkCredentials")]
        [HttpPost]
        public async Task<ActionResult> CheckCredentials([FromBody] UserDto request)
        {
            var responseFromDb = await accountServices.GetAccountByUsername(request.username);

            if (responseFromDb == null)
            {
                return BadRequest("User not found");
            }
            var user = responseFromDb.account;

            if (!BCrypt.Net.BCrypt.Verify(request.password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            return Ok();
        }

        [Route("getStatus")]
        [HttpGet]
        public ActionResult GetStatus()
        {
            return Ok();
        }

        private void SetResponseCookies(RefreshToken newRefreshToken, Account account)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Parse(newRefreshToken.Expires),
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            };

            Response.Cookies.Append("refresh_token", newRefreshToken.Token, cookieOptions);
            Response.Cookies.Append("username", account.Username, cookieOptions);
            account.RefreshToken = newRefreshToken.Token;
            account.TokenCreated = newRefreshToken.Created;
            account.TokenExpires = newRefreshToken.Expires;
            _ = accountServices.UpdateAccount(account);
        }

        private static RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now.ToString("s"),
                Expires = DateTime.Now.AddDays(7).ToString("s"),
            };

            return refreshToken;
        }

        private string GenerateAccessToken(Account account)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, "User")
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:Key").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private static UserResponse CreateUserResponse(Account account, List<DbPublication> dbPublications)
        {
            var response = new UserResponse
            {
                AccountId = account.AccountId,
                Username = account.Username,
                Favourites = dbPublications,
                RefreshToken = account.RefreshToken,
                TokenCreated = account.TokenCreated,
                TokenExpires = account.TokenExpires,
            };
            return response;
        }
    }
}
