using AutoMapper;
using LibraryWebAPI.Dtos.LoanDto;
using LibraryWebAPI.Dtos.RefreshToken;
using LibraryWebAPI.Dtos.Responses;
using LibraryWebAPI.Helpers;
using LibraryWebAPI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zheng.Infra.Data.Data;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Services
{
    public class TokenService: ITokenService
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtHelper _jwtHelper;

        public TokenService(LibraryDbContext context, IMapper mapper, JwtHelper jwtHelper)
        {
            _context = context;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        public async Task<CommonResponse> AddRefreshToken(RefreshToken_PostDto entity)
        {
            var token =  _mapper.Map<Token>(entity);

            //設定 refresh token 到期時間
            token.ExpiryDate = DateTime.Now.AddHours(10);
            token.CreatedBy = entity.AccountId;
            token.CreatedAt = DateTime.Now;

            await _context.Tokens.AddAsync(token);
            _context.SaveChanges();
            
            return new CommonResponse()
            {
                Success = true,
            };
        }


        public async Task<CommonResponse> UpdateRefreshToken(RefreshToken_PostDto entity)
        {
            var token = _context.Tokens.SingleOrDefault(x => x.RefreshToken == entity.RefreshToken);
           
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            if (token == null)
            {
                return new CommonResponse()
                {
                    Success = false,
                    Message = "Token不存在"
                };
            }

            var principal = _jwtHelper.GetPrincipalFromExpiredToken(entity.AccessToken);
            var allClaims = principal.Claims;
            //[Account]資料表[Id]
            var accountId = allClaims.FirstOrDefault(x => x.Type == "id");

            token.AccessToken = entity.AccessToken;
            token.UpdatedBy = Guid.Parse(accountId.Value);
            token.UpdatedAt = DateTime.Now;

            _context.Entry(token).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new CommonResponse()
            {
                Success = true,
            };
        }
    }
}
