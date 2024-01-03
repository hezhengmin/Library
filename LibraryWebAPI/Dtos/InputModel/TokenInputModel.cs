namespace LibraryWebAPI.Dtos.InputModel
{
    public class TokenInputModel
    {
        /// <summary>
        /// 原本 Token
        /// </summary>
        public string? AccessToken { get; set; }
        /// <summary>
        /// Refresh Token 自動重新驗證使用者並產生新的 JWT token
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}
