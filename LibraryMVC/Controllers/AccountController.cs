using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Account_Edit()
        {
            return View();
        }
    }
}
