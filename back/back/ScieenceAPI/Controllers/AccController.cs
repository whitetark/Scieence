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
        public async Task<Account> GetAccount(string id)
        {
            return await _accountServices.GetAccountById(id);
        }
        [Route("create")]
        [HttpPost]
        public async void AddAccount(Account account)
        {
            await _accountServices.AddAccount(account);
        }

        [Route("deleteById/{id}")]
        [HttpDelete]
        public async void DeleteAccount(string id)
        {
            await _accountServices.DeleteAccount(id);
        }

        [Route("update")]
        [HttpPut]
        public async void UpdateAccount(Account account)
        {
            await _accountServices.UpdateAccount(account);
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<Account>> Register([FromBody] UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
            var account = new Account
            {
                Username = request.username,
                PasswordHash = passwordHash,
                Favourites = new Response(),
            };
            await _accountServices.AddAccount(account);
            return Ok(account);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserDto request)
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

            string token = CreateToken(user);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);

            return Ok(token);
        }

        [Route("refresh-token")]
        [HttpPost]
        public async Task<ActionResult<string>> RefreshToken(UserDto request)
        {
            var user = await _accountServices.GetAccountByUsername(request.username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token");
            } else if(user.TokenExpires < DateTime.Now){
                return Unauthorized("Token expired");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user);
            return Ok(token);
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

        private void SetRefreshToken(RefreshToken newRefreshToken, Account account)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            account.RefreshToken = newRefreshToken.Token;
            account.TokenCreated = newRefreshToken.Created;
            account.TokenExpires = newRefreshToken.Expires;
            _ = _accountServices.UpdateAccount(account);
        }

        private string CreateToken(Account account)
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
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
