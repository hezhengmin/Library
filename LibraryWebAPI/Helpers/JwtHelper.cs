using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zheng.Infrastructure.Models;

namespace LibraryWebAPI.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(Account account)
        {
            //設定使用者資訊
            var claims = new List<Claim>
                {
                    new Claim("Id",account.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, account.AccountId)
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
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
