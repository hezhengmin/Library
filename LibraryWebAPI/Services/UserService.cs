using LibraryWebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace LibraryWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly string _account = "id";

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 目前登入的帳號id
        /// </summary>
        /// <returns></returns>
        public Guid GetCurrentAccountId()
        {
            string id = _httpContextAccessor.HttpContext.User.FindFirst(_account).Value;
            return new Guid(id);
        }
    }
}
