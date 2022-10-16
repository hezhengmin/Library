using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Book_Index()
        {
            return View();
        }

        public IActionResult Book_Edit()
        {
            return View();
        }

        public IActionResult Book_Import()
        {
            return View();
        }
    }
}
