using LibraryWebAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Zheng.Application.ViewModels.Account;

namespace LibraryWebAPI.Wappers
{
    public interface ISessionWapper
    {
        Account_SignInVM SingInAccount { get; set; }
    }

    public class SessionWapper : ISessionWapper
    {
        private static readonly string _accountKey = "session.account";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionWapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session;
            }
        }

        public Account_SignInVM SingInAccount
        {
            get
            {
                return Session.GetObject<Account_SignInVM>(_accountKey);
            }
            set
            {
                Session.SetObject(_accountKey, value);
            }
        }
    }
}
