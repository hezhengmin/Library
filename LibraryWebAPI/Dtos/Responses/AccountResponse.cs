using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.Responses
{
    public class AccountResponse : BaseResponse
    {
        public string JwtToken { get; set; }
        public object Account { get; set; }
    }
}
