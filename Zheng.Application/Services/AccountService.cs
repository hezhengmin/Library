
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zheng.Application.Dtos.Account;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;
using Zheng.Utilities.Compare;
using Zheng.Utilities.Cryptography;

namespace Zheng.Application.Services
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
        /// <param name="accountSignInVM"></param>
        /// <param name="accountEntity"></param>
        /// <returns></returns>
        public bool Add(Account_AddDto accountSignInVM, out Account accountEntity)
        {
            accountEntity = null;
            //帳號已存在，不能重複
            if (Exits(accountSignInVM.AccountId)) return false;

            byte[] hashBytes = SHAExtensions.PasswordSHA512Hash(accountSignInVM.Password);

            var account = new Account()
            {
                Id = Guid.NewGuid(),
                AccountId = accountSignInVM.AccountId,
                Password = hashBytes,
                SystemDate = DateTime.Now
            };

            accountEntity = account;

            try
            {
                _context.Account.Add(account);
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
            return await _context.Account.FindAsync(id);
        }

        public async Task<Account> Get(string accountId)
        {
            return await _context.Account.FirstOrDefaultAsync(x => x.AccountId == accountId);
        }

        public async Task<List<Account>> Get()
        {
            return await _context.Account.ToListAsync();
        }

        public bool Update(Account_UpdateDto vm)
        {
            var account = Get(vm.Id).Result;

            byte[] hashBytes = SHAExtensions.PasswordSHA512Hash(vm.Password);

            //更新欄位
            account.AccountId = vm.AccountId;
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
            return _context.Account.Any(x => x.Id == id);
        }

        public bool Exits(string accountId)
        {
            return _context.Account.Any(x => x.AccountId == accountId);
        }

        public void Remove(Guid id)
        {
            var entity = Get(id).Result;
            _context.Account.Remove(entity);
            _context.SaveChanges();
        }


        /// <summary>
        /// 登入驗證
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Account SingIn(Account_SignInDto entity)
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
