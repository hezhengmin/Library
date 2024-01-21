using System;

namespace LibraryWebAPI.Dtos.RefreshToken
{
    public class RefreshToken_PostDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevorked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
