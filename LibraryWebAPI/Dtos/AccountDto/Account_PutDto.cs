using System;

namespace LibraryWebAPI.Dtos.AccountDto
{
    /// <summary>
    /// 更新的帳號
    /// </summary>
    public class Account_PutDto
    {
        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public string Password { get; set; }
    }
}
