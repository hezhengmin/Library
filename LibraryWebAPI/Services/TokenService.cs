using AutoMapper;
using LibraryWebAPI.Dtos.LoanDto;
using LibraryWebAPI.Dtos.RefreshToken;
using LibraryWebAPI.Dtos.Responses;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly TokenValidationParameters _tokenValidationParams;

        public TokenService(LibraryDbContext context, IUserService userService, IMapper mapper, TokenValidationParameters tokenValidationParams)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _tokenValidationParams = tokenValidationParams;
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
            var token = await _context.Tokens.SingleOrDefaultAsync(x => x.RefreshToken == entity.RefreshToken);
           
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            if (token == null)
            {
                return new CommonResponse()
                {
                    Success = false,
                    Message = "Token不存在"
                };
            }

            token.AccessToken = entity.AccessToken;
            token.UpdatedBy = _userService.CurrentUserId;
            token.UpdatedAt = DateTime.Now;

            _context.Entry(token).State = EntityState.Modified;
            _context.SaveChanges();

            return new CommonResponse()
            {
                Success = true,
            };
        }
    }
}
