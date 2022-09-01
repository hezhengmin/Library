using LibraryWebAPI.Dtos.AccountDto;
using LibraryWebAPI.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LibraryWebAPI.ValidationAttributes
{
    /// <summary>
    /// 確認舊密碼輸入無誤
    /// </summary>
    public class CheckOldPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            AccountService _accountService = (AccountService)validationContext.GetService(typeof(AccountService));

            var dto = (Account_ResetPasswordDto)value;

            var isOldPassword = _accountService.CheckOldPassowrd(dto.Id, dto.OldPassword).Result;
            if (!isOldPassword)
            {
                return new ValidationResult("舊密碼輸入錯誤", new[] { "OldPassowrd" });
            }

            return ValidationResult.Success;
        }
    }
}
