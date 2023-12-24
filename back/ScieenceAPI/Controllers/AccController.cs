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
    [Authorize(Roles ="User, Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AccController(AccountServices accountServices, IConfiguration configuration, PublicationServices pubServices, FavouriteServices favouriteServices) : ControllerBase
    {
        [Route("getByUsername")]
        [HttpGet]
        public async Task<ActionResult> GetAccount()
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
            var responseFromDb = await accountServices.GetAccountByUsername(updateDto.Username);
            if(responseFromDb.account == null)
            {
                return BadRequest("User not found");
            }

            var newAccount = new Account
            {
                AccountId = updateDto.Id,
                Username = updateDto.Username,
                PasswordHash = responseFromDb.account.PasswordHash,
                RefreshToken = updateDto.RefreshToken,
                TokenCreated = updateDto.TokenCreated,
                TokenExpires = updateDto.TokenExpires,
            };

            await accountServices.UpdateAccount(newAccount);
            return Ok(newAccount);
        }

        [Route("addPublicationToAccount")]
        [HttpPut]
        public async Task<ActionResult> AddPublicationToAccount(AddPublicationToAccountDto request)
        {
            var responseFromDb = await accountServices.GetAccountByUsername(request.account.Username);
            if (responseFromDb == null)
            {
                return BadRequest("User not found");
            }

            var publication = await pubServices.CreatePublication(request.publicationToAdd);
            await favouriteServices.AddFavorite(responseFromDb.account.AccountId, publication.PublicationId);
            var publications = await favouriteServices.GetFavoritesByUsername(responseFromDb.account.Username);
            var result = CreateUserResponse(new AccountResponse(responseFromDb.account, publications));
            return Ok(result);
        }

        [Route("removePublicationFromAccount")]
        [HttpPut]
        public async Task<ActionResult> removePublicationFromAccount(AddPublicationToAccountDto request)
        {
            var responseFromDb = await accountServices.GetAccountByUsername(request.account.Username);
            if (responseFromDb == null)
            {
                return BadRequest("User not found");
            }

            var publication = await pubServices.GetPublicationByUrl(request.publicationToAdd.Url);
            await favouriteServices.DeleteFavorite(responseFromDb.account.AccountId, publication.PublicationId);
            var publications = await favouriteServices.GetFavoritesByUsername(responseFromDb.account.Username);
            var result = CreateUserResponse(new AccountResponse(responseFromDb.account, publications));
            return Ok(result);
        }

        [Route("changePassword")]
        [HttpPatch]
        public async Task<ActionResult> ChangePassword([FromBody] UserDto request)
        {
            var responseFromDb = await accountServices.GetAccountByUsername(request.username);

            if (responseFromDb == null)
            {
                return BadRequest("User not found");
            }
            var user = responseFromDb.account;
            string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.password);

            user.PasswordHash = newPasswordHash;
            await accountServices.UpdateAccount(user);

            return Ok();
        }
        private static UserResponse CreateUserResponse(AccountResponse responseFromDb)
        {
            var account = responseFromDb.account;
            var response = new UserResponse
            {
                AccountId = account.AccountId,
                Username = account.Username,
                Favourites = responseFromDb.publications,
                RefreshToken = account.RefreshToken,
                TokenCreated = account.TokenCreated,
                TokenExpires = account.TokenExpires,
            };
            return response;
        }
    }
}
