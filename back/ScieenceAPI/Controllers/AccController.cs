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
    public class AccController(AccountServices accountServices, IConfiguration configuration) : ControllerBase
    {
        [Route("getByUsername")]
        [HttpGet]
        public async Task<ActionResult<Account>> GetAccount()
        {
            var username = Request.Cookies["username"];
            if(username == null)
            {
                return BadRequest("No username cookie");
            }

            var user = await accountServices.GetAccountByUsername(username);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            var result = CreateUserResponse(user);
            return Ok(result);
        }

        [Route("deleteById/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAccount(string id)
        {
            await accountServices.DeleteAccount(id);
            return Ok();
        }

        [Route("update")]
        [HttpPut]
        public async Task<ActionResult> UpdateAccount(AccountUpdateDto updateDto)
        {
            var account = await accountServices.GetAccountByUsername(updateDto.Username);
            if(account == null)
            {
                return BadRequest("User not found");
            }

            var newAccount = new Account
            {
                Id = updateDto.Id,
                Username = updateDto.Username,
                PasswordHash = account.PasswordHash,
                Favourites = updateDto.Favourites,
                RefreshToken = updateDto.RefreshToken,
                TokenCreated = updateDto.TokenCreated,
                TokenExpires = updateDto.TokenExpires,
            };

            await accountServices.UpdateAccount(newAccount);
            return Ok();
        }

  
        [Route("changePassword")]
        [HttpPatch]
        public async Task<ActionResult> ChangePassword([FromBody] UserDto request)
        {
            var user = await accountServices.GetAccountByUsername(request.username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.password);

            user.PasswordHash = newPasswordHash;
            await accountServices.UpdateAccount(user);

            return Ok();
        }

        private static UserResponse CreateUserResponse(Account account)
        {
            var response = new UserResponse
            {
                Id = account.Id,
                Username = account.Username,
                Favourites = account.Favourites,
                RefreshToken = account.RefreshToken,
                TokenCreated = account.TokenCreated,
                TokenExpires = account.TokenExpires,

            };
            return response;
        }
    }
}
