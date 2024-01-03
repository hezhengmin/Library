using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Helpers
{
    public class JwtHelper
    {
        /// <summary>
        /// Token 過期時間(分)
        /// </summary>
        private static int keepTime = 1;

        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(Account account)
        {
            //建立使用者資訊 Claim
            var claims = new List<Claim>
                {
                    new Claim("id",account.Id.ToString()),//主key
                    new Claim("account",account.UserId.ToString()),//帳號
                    new Claim(JwtRegisteredClaimNames.Email, account.Email),
                    //new Claim("role", "admin")
                };

            //取出appsettings.json裡的KEY處理
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //設定jwt相關資訊
            var jwt = new JwtSecurityToken
            (
                issuer: _configuration["JWT:Issuer"], //發行者
                audience: _configuration["JWT:Audience"], //接收者
                claims: claims,
                expires: DateTime.Now.AddMinutes(keepTime),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }



        /// <summary>
        /// 從過期的token中取得用戶主體
        /// </summary>
        /// <param name="token">accessToken</param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"])),
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:Audience"],
                ValidateLifetime = false, // 禁用過期時間驗證以取得過期的 Token
                ClockSkew = TimeSpan.Zero // 將時鐘偏移設為零，以進行精確的過期時間比較
            };

            SecurityToken securityToken;
            ClaimsPrincipal principal = null;

            try
            {
                principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
            }
            catch (SecurityTokenException ex)
            {
                throw new SecurityTokenException($"invalid token: {ex}");
            }

            return principal;
        }
    }
}
