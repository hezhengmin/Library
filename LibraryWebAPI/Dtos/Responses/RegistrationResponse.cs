using LibraryWebAPI.Dtos.AccountDto;
using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.Responses
{
    public class RegistrationResponse : BaseResponse
    {
        public RegistrationResponse() : base() { }

        /// <summary>
        /// 註冊的帳號
        /// </summary>
        public Account_GetDto RegAccount { get; set; }

    }
}
