using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebAPI.Dtos.Account
{
    /// <summary>
    /// 新增的帳號
    /// </summary>
    public class Account_AddDto
    {
        public string AccountId { get; set; }
        public string Password { get; set; }
    }
}
