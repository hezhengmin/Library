﻿using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Loan_Index()
        {
            return View();
        }
    }
}
