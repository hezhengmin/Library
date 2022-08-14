using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Abstract.Account
{
    public abstract class Account_Dto_Base
    {
        [Required]
        [MaxLength(20)]
        public virtual string AccountId { get; set; }
        [Required]
        public virtual string Password { get; set; }
    }
}
