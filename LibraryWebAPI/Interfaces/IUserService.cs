using System;

namespace LibraryWebAPI.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 目前登入的帳號id
        /// </summary>
        Guid CurrentAccountId { get; }
    }
}