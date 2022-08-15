using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.Responses
{
    public class AccountResponse
    {
        public string JwtToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public object account { get; set; }
    }
}
