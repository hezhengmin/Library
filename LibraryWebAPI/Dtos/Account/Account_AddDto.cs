using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebAPI.Dtos.Account
{
    /// <summary>
    /// 新增的帳號
    /// </summary>
    public class Account_AddDto : Account_Dto_Base
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
