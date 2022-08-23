using System;

namespace LibraryWebAPI.Dtos.Account
{
    public class Account_GetDto
    {
        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
    }
}
