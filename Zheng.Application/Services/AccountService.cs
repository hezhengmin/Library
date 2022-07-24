using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zheng.Application.ViewModels;
using Zheng.Application.ViewModels.Account;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;

namespace Zheng.Application.Services
{
    public class AccountService
    {
        private readonly LibraryDbContext _context;

        public AccountService(LibraryDbContext context)
        {
            _context = context;
        }

        public byte[] PasswordSHA512Hash(string password)
        {
            byte[] hashBytes = new byte[64];
            using (SHA512 sha512Hash = SHA512.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                hashBytes = sha512Hash.ComputeHash(sourceBytes);
            }

            return hashBytes;
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="accountSignInVM"></param>
        /// <param name="accountEntity"></param>
        /// <returns></returns>
        public bool Add(Account_AddVM accountSignInVM, out Account accountEntity)
        {

            byte[] hashBytes = PasswordSHA512Hash(accountSignInVM.Password);

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

        public async Task<List<Account>> Get()
        {
            return await _context.Account.ToListAsync();
        }

        public bool Update(Account_UpdateVM vm)
        {
            var account = Get(vm.Id).Result;

            byte[] hashBytes = PasswordSHA512Hash(vm.Password);

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

        public void Remove(Guid id)
        {
            var entity = Get(id).Result;
            _context.Account.Remove(entity);
            _context.SaveChanges();
        }
    }
}
