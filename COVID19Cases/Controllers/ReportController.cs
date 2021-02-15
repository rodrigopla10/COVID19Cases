using COVID19Cases.Helper;
using COVID19Cases.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace COVID19Cases.Controllers
{
    public class ReportController : Controller
    {
        private readonly IOperations _operations;

        public ReportController(IOperations operations)
        {
            _operations = operations;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<TopRegion> regions = await _operations.GetTopRegionCovidAsync();
                return View(regions);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
