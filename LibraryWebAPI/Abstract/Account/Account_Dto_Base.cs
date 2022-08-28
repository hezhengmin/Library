using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Abstract.Account
{
    public abstract class Account_Dto_Base
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public virtual string AccountId { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        /// </summary>
        public virtual string Password { get; set; }
    }
}
