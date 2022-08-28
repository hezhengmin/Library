using LibraryWebAPI.Dtos.AccountDto;
using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.Responses
{
    public class RegistrationResponse : BaseResponse
    {
        public RegistrationResponse()
        {
            this.Errors = new List<string>();
        }
        /// <summary>
        /// 註冊的帳號
        /// </summary>
        public Account_GetDto RegAccount { get; set; }

    }
}
