using Database.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class AccountServices
    {
        private readonly IMongoCollection<Account> _accounts;

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
    }
}
