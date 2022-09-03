using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Dtos.AccountDto
{
    /// <summary>
    /// 忘記密碼(寄信)
    /// </summary>
    public class Account_ForgetPasswordDto
    {
        [Required(ErrorMessage = "帳號必填")]
        public string AccountId { get; set; }

        [Required(ErrorMessage = "Email必填")]
        public string Email { get; set; }
    }
}
