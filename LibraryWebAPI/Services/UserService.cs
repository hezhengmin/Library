using LibraryWebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace LibraryWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// PAYLOAD 的內部資訊 id，是Account資料表的主鍵
        /// </summary>
        private static readonly string _accountKey = "id";

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 目前登入的帳號id
        /// </summary>
        public Guid CurrentUserId
        {
            get
            {
                string id = _httpContextAccessor.HttpContext.User.FindFirst(_accountKey).Value;
                return Guid.Parse(id);
            }
        }
    }
}
