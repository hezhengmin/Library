using LibraryWebAPI.Abstract.Account;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Dtos.AccountDto
{
    /// <summary>
    /// 新增的帳號
    /// </summary>
    public class Account_PostDto : Account_Dto_Base
    {
        [Required(ErrorMessage = "帳號必填")]
        [MaxLength(20)]
        public override string AccountId { get; set; }
        [Required(ErrorMessage = "密碼必填")]
        public override string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
