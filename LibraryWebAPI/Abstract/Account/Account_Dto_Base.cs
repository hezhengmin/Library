using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Abstract.Account
{
    public abstract class Account_Dto_Base
    {
        [Required(ErrorMessage = "帳號必填")]
        [MaxLength(20)]
        public virtual string AccountId { get; set; }
        [Required(ErrorMessage = "密碼必填")]
        public virtual string Password { get; set; }
    }
}
