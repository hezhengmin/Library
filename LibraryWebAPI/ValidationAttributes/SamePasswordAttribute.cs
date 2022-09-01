using LibraryWebAPI.Dtos.AccountDto;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.ValidationAttributes
{
    public class SamePasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dto = (Account_ResetPasswordDto)value;

            if (dto.NewPassword != dto.ConfirmPassword)
            {
                return new ValidationResult("新密碼要等於確認密碼", new[] { "ConfirmPassword" });
            }

            return ValidationResult.Success;
        }
    }
}
