using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19Cases.Controllers
{
    public class ExportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
