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
    public class AccountServices(DbClient dbClient)
    {
        private readonly IMongoCollection<Account> _accounts = dbClient.GetAccountCollection();

        public async Task<List<Account>> GetAccs()
        {
            try
            {
                return await _accounts.Find(account => true).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Account> GetAccountById(string id)
        {
            try
            {
                return await _accounts.Find(account => account.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Account> GetAccountByUsername(string username)
        {
            try
            {
                return await _accounts.Find(account => account.Username == username).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AddAccount(Account account)
        {
            try
            {
                await _accounts.InsertOneAsync(account);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DeleteResult> DeleteAccount(string id)
        {
            try
            {
                return await _accounts.DeleteOneAsync(account => account.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ReplaceOneResult> UpdateAccount(Account newAccount)
        {
            try
            {
                //await GetAccount(newAccount.Id);
                return await _accounts.ReplaceOneAsync(b => b.Id == newAccount.Id, newAccount);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
