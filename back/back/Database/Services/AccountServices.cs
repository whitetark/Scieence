using Database.Models;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class AccountServices
    {
        private readonly IMongoCollection<Account> _accounts;
        private readonly string key = "ScieenceSecurityKeyTopSecret";

        public AccountServices(DbClient dbClient)
        {
            _accounts = dbClient.GetAccountCollection();
        }
        public async Task<List<Account>> GetAccs()
        {
            try
            {
                return await _accounts.Find(account => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Account> GetAccount(string id)
        {
            try
            {
                return await _accounts.Find(account => account.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddAccount(Account account)
        {
            try
            {
                await _accounts.InsertOneAsync(account);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DeleteResult> DeleteAccount(string id)
        {
            try
            {
                return await _accounts.DeleteOneAsync(account => account.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReplaceOneResult> UpdateAccount(Account newAccount)
        {
            try
            {
                await GetAccount(newAccount.Id);
                return await _accounts.ReplaceOneAsync(b => b.Id == newAccount.Id, newAccount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string? Authenticate(string username, string password)
        {
            var user = _accounts.Find(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
