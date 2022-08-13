using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Dtos.Account
{
    public class Account_Dto_Base
    {
        [Required]
        [MaxLength(20)]
        public string AccountId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
