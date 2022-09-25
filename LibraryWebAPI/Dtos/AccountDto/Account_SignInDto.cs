using LibraryWebAPI.Abstract.Account;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Dtos.AccountDto
{
    /// <summary>
    /// 登入帳密
    /// </summary>
    public class Account_LoginDto : Account_Dto_Base
    {
        [Required(ErrorMessage = "帳號必填")]
        [MaxLength(20)]
        public override string UserId { get; set; }
        [Required(ErrorMessage = "密碼必填")]
        public override string Password { get; set; }
    }
}
