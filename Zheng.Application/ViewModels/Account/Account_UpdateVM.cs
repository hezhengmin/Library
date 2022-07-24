using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zheng.Application.ViewModels.Account
{
    /// <summary>
    /// 更新的帳號
    /// </summary>
    public class Account_UpdateVM
    {
        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public string Password { get; set; }
    }
}
