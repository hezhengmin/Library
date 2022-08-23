using LibraryWebAPI.Abstract.Account;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Dtos.Account
{
    /// <summary>
    /// 新增的帳號
    /// </summary>
    public class Account_PostDto : Account_Dto_Base
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
