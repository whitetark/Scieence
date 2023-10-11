using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScieenceAPI.Models;

namespace ScieenceAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccController : ControllerBase
    {
        private readonly AccountServices _accountServices;
        public AccController(AccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        [HttpGet("getById/{id}")]
        public async Task<Account> GetAccount(string id)
        {
            return await _accountServices.GetAccount(id);
        }
        [AllowAnonymous]
        [HttpPost("create")]
        public async void AddAccount(Account account)
        {
            await _accountServices.AddAccount(account);
        }

        [HttpDelete("deleteById/{id}")]
        public async void DeleteAccount(string id)
        {
            await _accountServices.DeleteAccount(id);
        }

        [HttpPut("update")]
        public async void UpdateAccount(Account account)
        {
            await _accountServices.UpdateAccount(account);
        }

        [AllowAnonymous]
        [Route("auth")]
        [HttpPost]
        public ActionResult Login([FromBody] Auth auth)
        {
            var token = _accountServices.Authenticate(auth.username, auth.password);

            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(new { token, auth });
        }
    }
}
