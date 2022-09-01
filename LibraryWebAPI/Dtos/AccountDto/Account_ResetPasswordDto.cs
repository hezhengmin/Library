using System.ComponentModel.DataAnnotations;
using System;
using LibraryWebAPI.ValidationAttributes;

namespace LibraryWebAPI.Dtos.AccountDto
{
    [SamePassword]
    [CheckOldPassword]
    public class Account_ResetPasswordDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "舊密碼必填")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "新密碼必填")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "確認密碼必填")]
        public string ConfirmPassword { get; set; }
    }
}
