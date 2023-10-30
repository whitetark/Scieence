using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScieenceAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ScieenceAPI.Controllers
{
    [Authorize(Roles ="User, Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AccController : ControllerBase
    {
        private readonly AccountServices _accountServices;
        private readonly IConfiguration _configuration;
        public AccController(AccountServices accountServices, IConfiguration configuration)
        {
            _accountServices = accountServices;
            _configuration = configuration;
        }
        [Route("getById/{id}")]
        [HttpGet]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            return Ok(await _accountServices.GetAccountById(id));
        }

        [Route("getByUsername")]
        [HttpGet]
        public async Task<ActionResult<Account>> GetAccount()
        {
            var username = Request.Cookies["username"];
            if(username == null)
            {
                return BadRequest("No username cookie");
            }

            var user = await _accountServices.GetAccountByUsername(username);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user);
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult> AddAccount(Account account)
        {
            await _accountServices.AddAccount(account);
            return Ok();
        }

        [Route("deleteById/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAccount(string id)
        {
            await _accountServices.DeleteAccount(id);
            return Ok();
        }

        [Route("update")]
        [HttpPut]
        public async Task<ActionResult> UpdateAccount(Account account)
        {
            await _accountServices.UpdateAccount(account);
            return Ok();
        }

        [Route("checkCredentials")]
        [HttpGet]
        public async Task<ActionResult> CheckCredentials([FromBody] UserDto request)
        {
            var user = await _accountServices.GetAccountByUsername(request.username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            return Ok();
        }


        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<Account>> Register([FromBody] UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
            var user = new Account
            {
                Username = request.username,
                PasswordHash = passwordHash,
                Favourites = new Response(),
            };
            await _accountServices.AddAccount(user);

            string token = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            SetResponseCookies(refreshToken, user);

            return Ok(new { token, user });
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<object>> Login([FromBody] UserDto request)
        {
            var user = await _accountServices.GetAccountByUsername(request.username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if(!BCrypt.Net.BCrypt.Verify(request.password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            SetResponseCookies(refreshToken, user);

            return Ok(new { token, user });
        }
        [AllowAnonymous]
        [Route("logout")]
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            var username = Request.Cookies["username"];
            if (username == null)
            {
                return BadRequest("User not found");
            }
            var user = await _accountServices.GetAccountByUsername(username);

            user.RefreshToken = "";
            user.TokenExpires = DateTime.UtcNow;
            user.TokenCreated = DateTime.UtcNow;
            _ = _accountServices.UpdateAccount(user);

            Response.Cookies.Delete("username"); 
            Response.Cookies.Delete("refresh_token");

            return Ok();
        }

        [AllowAnonymous]
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

            var user = await _accountServices.GetAccountByUsername(username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return BadRequest("Wrong refresh token");
            }

            if(user.TokenExpires < DateTime.Now){
                return BadRequest("Token expired");
            }

            string token = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetResponseCookies(newRefreshToken, user);
            return Ok(new { token, user });
        }

        private void SetResponseCookies(RefreshToken newRefreshToken, Account account)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = newRefreshToken.Expires,
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            };

            Response.Cookies.Append("refresh_token", newRefreshToken.Token, cookieOptions);
            Response.Cookies.Append("username", account.Username, cookieOptions);
            account.RefreshToken = newRefreshToken.Token;
            account.TokenCreated = newRefreshToken.Created;
            account.TokenExpires = newRefreshToken.Expires;
            _ = _accountServices.UpdateAccount(account);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
            };

            return refreshToken;
        }

        private string GenerateAccessToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
