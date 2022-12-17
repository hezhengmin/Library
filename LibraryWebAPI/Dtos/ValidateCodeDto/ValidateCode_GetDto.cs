namespace LibraryWebAPI.Dtos.ValidateCodeDto
{
    public class ValidateCode_GetDto
    {
        /// <summary>
        /// 驗證碼圖片
        /// </summary>
        public string Base64 { get; set; }
        /// <summary>
        /// 驗證碼雜湊
        /// </summary>
        public string Hash { get; set; }
    }
}
