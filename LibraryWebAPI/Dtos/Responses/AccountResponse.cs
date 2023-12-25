using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.Responses
{
    public class AccountResponse : BaseResponse
    {
        public string RefreshToken { get; set; }
        public string JwtToken { get; set; }
        public object Account { get; set; }
    }
}
