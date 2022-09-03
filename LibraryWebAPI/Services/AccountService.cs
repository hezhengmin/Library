using LibraryWebAPI.Dtos.AccountDto;
using LibraryWebAPI.Dtos.Responses;
using LibraryWebAPI.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;
using Zheng.Utilities.Compare;
using Zheng.Utilities.Cryptography;
using Zheng.Utilities.Randomized;

namespace LibraryWebAPI.Services
{
    public class AccountService
    {
        private readonly LibraryDbContext _context;
        private readonly EmailSenderHelper _emailSenderHelper;
        public AccountService(LibraryDbContext context, EmailSenderHelper emailSenderHelper)
        {
            _context = context;
            _emailSenderHelper = emailSenderHelper;
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="accountAddEntity"></param>
        /// <param name="accountEntity"></param>
        /// <returns></returns>
        public async Task<RegistrationResponse> Add(Account_PostDto accountAddEntity)
        {
            var response = new RegistrationResponse();

            //帳號已存在，不能重複
            var exitsAccountId = await Exits(accountAddEntity.AccountId);
            if (exitsAccountId)
            {
                response.Success = false;
                response.Errors.Add("帳號已存在，不能重複");
                return response;
            }

            //Email已存在，不能重複
            var exitsEmail = await ExitsEmail(accountAddEntity.Email);
            if (exitsEmail)
            {
                response.Success = false;
                response.Errors.Add("Email已存在，不能重複");
                return response;
            }


            byte[] hashBytes = SHAExtensions.PasswordSHA512Hash(accountAddEntity.Password);

            var account = new Account()
            {
                Id = Guid.NewGuid(),
                AccountId = accountAddEntity.AccountId,
                Password = hashBytes,
                Email = accountAddEntity.Email,
                SystemDate = DateTime.Now
            };

            try
            {
                await _context.Accounts.AddAsync(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //新增失敗
                response.Success = false;
                response.Errors.Add($"資料庫新增失敗，{ex.ToString()}");
                return response;
            }

            response.RegAccount = new Account_GetDto()
            {
                Id = account.Id,
                AccountId = account.AccountId,
                Email = account.Email
            };
            response.Success = true;
            response.Errors.Clear();
            return response;
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

        public async Task<Account_GetDto> GetDto(Guid id)
        {
            return await _context.Accounts
                .Select(x => new Account_GetDto { Id = x.Id, AccountId = x.AccountId, Email = x.Email })
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


        public async Task<Account> GetByEmail(string email)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x => x.Email == email);
        
        }
        public async Task<List<Account_GetDto>> Get()
        {
            return await _context.Accounts
                .Select(x => new Account_GetDto
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    Email = x.Email
                }
            ).ToListAsync();
        }

        public async Task<bool> Update(Guid id, JsonPatchDocument<Account> patchEntity)
        {
            var account = await Get(id);
            //無此帳號
            if (account == null) return false;

            patchEntity.ApplyTo(account);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Account_PutDto entity)
        {
            var account = await Get(entity.Id);
            //無此帳號
            if (account == null) return false;

            //更改帳號id
            if (account.AccountId != entity.AccountId)
            {
                //帳號已存在，不能重複
                var result = await Exits(entity.AccountId);
                if (result) return false;
            }

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

        public async Task<bool> ExitsEmail(string email)
        {
            return await _context.Accounts.AnyAsync(x => x.Email == email);
        }

        public async Task Delete(Guid id)
        {
            var entity = await Get(id);
            _context.Accounts.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// 登入驗證
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Account> Login(Account_LoginDto entity)
        {
            var account = await Get(entity.AccountId);

            //沒有該帳號，直接回傳false
            if (account == null) return null;

            //登入密碼加密
            var secondByteArray = SHAExtensions.PasswordSHA512Hash(entity.Password);

            //跟資料庫的，該帳號的密碼比對
            if (!account.Password.CompareByteArray(secondByteArray)) return null;

            return account;
        }


        /// <summary>
        /// 驗證舊密碼
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> CheckOldPassowrd(Guid id, string password)
        {

            var account = await Get(id);
            //無帳號，回傳false
            if (account == null) return false;
            //舊密碼
            byte[] hashBytes = SHAExtensions.PasswordSHA512Hash(password);

            if(!account.Password.CompareByteArray(hashBytes)) return false;

            return true;
        }

        /// <summary>
        /// 更新密碼
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(Account_ResetPasswordDto entity)
        {
            var account = await Get(entity.Id);
            //無此帳號
            if (account == null) return false;

            byte[] hashBytes = SHAExtensions.PasswordSHA512Hash(entity.NewPassword);

            //更新欄位
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


        /// <summary>
        /// 忘記密碼(寄信給新的亂數密碼)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ForgetPasswordResponse> ForgetPassword(Account_ForgetPasswordDto entity)
        {
            var response = new ForgetPasswordResponse();
            var account = await GetByEmail(entity.Email);

            //無此帳號
            if (account == null)
            {
                response.Success = false;
                response.Errors.Add("帳號不存在");
                return response;
            }

            string newPassword = RandomExtensions.GetRandom();
            byte[] hashBytes = SHAExtensions.PasswordSHA512Hash(newPassword);

            var subject = "【Library】申請重設登入密碼回覆";
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"親愛的Library會員 {account.AccountId} 你好：\n");
            sb.AppendLine($"重新設定您的Library密碼：{newPassword}");
            sb.AppendLine("再以此密碼登入Library。");
            sb.AppendLine("感謝您對Library的支持與愛護。\n\n");
            sb.AppendLine("Library客服人員 敬上\n\n");
            sb.AppendLine("--");
            sb.AppendLine("※ 此信件為系統發出信件，請勿直接回覆，感謝您的配合。謝謝！※");

            var isSendSucess = _emailSenderHelper.Send(subject, sb.ToString(), account.AccountId, account.Email);

            //寄信有無成功
            if (!isSendSucess)
            {
                response.Success = false;
                response.Errors.Add("寄信不成功");
                return response;
            }

            //更新欄位
            account.Password = hashBytes;
            
            try
            {
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                //新增失敗
                response.Success = false;
                response.Errors.Add($"資料庫更新失敗，{ex.ToString()}");
                return response;
            }

            response.Success = true;
            return response;
        }

    }
}
