using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWebAPI.Dtos.Account;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;
using Zheng.Utilities.Compare;
using Zheng.Utilities.Cryptography;

namespace LibraryWebAPI.Services
{
    public class AccountService
    {
        private readonly LibraryDbContext _context;

        public AccountService(LibraryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="accountAddEntity"></param>
        /// <param name="accountEntity"></param>
        /// <returns></returns>
        public bool Add(Account_AddDto accountAddEntity, out Account accountEntity)
        {
            accountEntity = null;
            //帳號已存在，不能重複
            if (Exits(accountAddEntity.AccountId).Result) return false;

            byte[] hashBytes = SHAExtensions.PasswordSHA512Hash(accountAddEntity.Password);

            var account = new Account()
            {
                Id = Guid.NewGuid(),
                AccountId = accountAddEntity.AccountId,
                Password = hashBytes,
                Email = accountAddEntity.Email,
                SystemDate = DateTime.Now
            };

            accountEntity = account;

            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //新增失敗
                return false;
            }

            return true;
        }

        public async Task<Account> Get(Guid id)
        {
            return await _context.Accounts.FindAsync(id);
            #region SQL 查詢語法
            /*
            exec sp_executesql N'SELECT TOP(1) [a].[Id], [a].[AccountId], [a].[Password], [a][SystemDate]
            FROM [Account] AS [a]
            WHERE [a].[Id] = @__p_0',N'@__p_0 uniqueidentifier',@__p_0='6B568F00-ECD6-4C69-94C9-C812DC574B98'
            */
            #endregion
        }

        public async Task<Account_Dto> GetDto(Guid id)
        {
            return await _context.Accounts
                .Select(x => new Account_Dto { Id = x.Id, AccountId = x.AccountId ,Email = x.Email})
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> Get(string accountId)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x => x.AccountId == accountId);
            #region SQL 查詢語法 FirstOrDefaultAsync
            /*
            exec sp_executesql N'SELECT TOP(1) [a].[Id], [a].[AccountId], [a].[Password], [a].[SystemDate]
            FROM[Account] AS[a]
            WHERE[a].[AccountId] = @__accountId_0',N'@__accountId_0 varchar(20)',@__accountId_0='admin'
            */
            #endregion
            #region SQL 查詢語法 SingleOrDefaultAsync，兩個帳號會發生錯誤
            /*
            exec sp_executesql N'SELECT TOP(2) [a].[Id], [a].[AccountId], [a].[Password], [a].[SystemDate]
            FROM [Account] AS [a]
            WHERE [a].[AccountId] = @__accountId_0',N'@__accountId_0 varchar(20)',@__accountId_0='admin'*/
            #endregion
        }

        public async Task<List<Account_Dto>> Get()
        {
            return await _context.Accounts
                .Select(x => new Account_Dto
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    Email = x.Email
                }
            ).ToListAsync();
        }

        public bool Update(Account_UpdateDto entity)
        {
            var account = Get(entity.Id).Result;

            //無此帳號
            if (account == null) return false;
            //帳號已存在，不能重複
            if (Exits(entity.AccountId).Result) return false;

            byte[] hashBytes = SHAExtensions.PasswordSHA512Hash(entity.Password);

            //更新欄位
            account.AccountId = entity.AccountId;
            account.Password = hashBytes;

            try
            {
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return false;
            }

            return true;
        }

        public bool Check(Guid id)
        {
            return _context.Accounts.Any(x => x.Id == id);
        }

        public async Task<bool> Exits(string accountId)
        {
            return await _context.Accounts.AnyAsync(x => x.AccountId == accountId);
        }

        public void Remove(Guid id)
        {
            var entity = Get(id).Result;
            _context.Accounts.Remove(entity);
            _context.SaveChanges();
        }


        /// <summary>
        /// 登入驗證
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Account Login(Account_LoginDto entity)
        {
            var account = Get(entity.AccountId).Result;

            //沒有該帳號，直接回傳false
            if (account == null) return null;

            //登入密碼加密
            var secondByteArray = SHAExtensions.PasswordSHA512Hash(entity.Password);

            //跟資料庫的，該帳號的密碼比對
            if (!account.Password.CompareByteArray(secondByteArray)) return null;

            return account;
        }

    }
}
