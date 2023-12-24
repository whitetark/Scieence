using Dapper;
using Database.Models;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class AccountServices(DbClient dbClient, FavouriteServices favouriteServices)
    {
        private readonly IDbConnection _pubDbConnection = dbClient.GetPubDbConnection();

        //public async Task<IEnumerable<T>> LoadData<T>(string sql)
        //{
        //    return await _pubDbConnection.QueryAsync<T>(sql);
        //}
        //public async Task<T> LoadDataSingle<T>(string sql)
        //{
        //    return await _pubDbConnection.QuerySingleAsync<T>(sql);
        //}

        //public async Task<bool> ExecuteSql(string sql)
        //{
        //    return await _pubDbConnection.ExecuteAsync(sql) > 0;
        //}

        //public async Task<int> ExecuteSqlWithRowCount(string sql)
        //{
        //    return await _pubDbConnection.ExecuteAsync(sql);
        //}

        public async Task<AccountResponse> GetAccountByUsername(string username)
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Accounts
                WHERE Username = @username";
                var account = await _pubDbConnection.QuerySingleAsync<Account>(sql, new { username });
                var publications = await favouriteServices.GetFavoritesByUsername(account.Username);
                return new AccountResponse(account, publications);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to Get Account", ex);
            }
        }
        public async Task<Account> AddAccount(Account account)
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Accounts
                WHERE Username = @username";

                var test = await _pubDbConnection.QueryAsync<Account>(sql, new { username = account.Username });
                
                if(test.Count() > 0)
                {
                    return test.FirstOrDefault();
                }

                sql = @"INSERT INTO PublicationSchema.Accounts
                ([Username],[PasswordHash],[RefreshToken],[TokenCreated],[TokenExpires]) 
                OUTPUT INSERTED.*
                VALUES (@username, @passwordHash, @refreshToken, @tokenCreated, @tokenExpires)";

                return await _pubDbConnection.QuerySingleAsync<Account>(sql, new
                {
                    username = account.Username,
                    passwordHash = account.PasswordHash,
                    refreshToken = account.RefreshToken,
                    tokenCreated = account.TokenCreated,
                    tokenExpires = account.TokenExpires
                });
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public async Task DeleteAccount(string id)
        {
            string sql = @"DELETE FROM PublicationSchema.Accounts
            WHERE AccountId = @id";

            if (await _pubDbConnection.ExecuteAsync(sql, new { id }) > 0) { return; }

            throw new Exception("Failed to Delete Account");
        }
        public async Task UpdateAccount(Account newAccount)
        {
            string sql = @"UPDATE PublicationSchema.Accounts 
            SET [PasswordHash] = @passwordHash, [RefreshToken] = @refreshToken, [TokenCreated] = @tokenCreated, [TokenExpires] = @tokenExpires
            WHERE Username = @username";

            if (await _pubDbConnection.ExecuteAsync(sql, new { passwordHash = newAccount.PasswordHash, refreshToken = newAccount.RefreshToken, tokenCreated = newAccount.TokenCreated, tokenExpires = newAccount.TokenExpires, username = newAccount.Username }) > 0) { return ; }

            throw new Exception("Failed to Update Publication");
        }
    }
}
