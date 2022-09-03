using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 帳號編輯(密碼、信箱)
        /// </summary>
        /// <returns></returns>
        public IActionResult Account_Edit()
        {
            return View();
        }
    }
}
