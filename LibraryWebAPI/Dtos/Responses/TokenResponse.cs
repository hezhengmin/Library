namespace LibraryWebAPI.Dtos.Responses
{
    public class TokenResponse : BaseResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        /// <summary>
        /// 原本 Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Refresh Token 自动重新验证用户并生成新的 JWT token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
