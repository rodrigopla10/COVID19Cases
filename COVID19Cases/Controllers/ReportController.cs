﻿using COVID19Cases.Helper;
using COVID19Cases.Models;
using COVID19Cases.ViewModel;
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
                TopRegionViewModel vModelTop = new TopRegionViewModel();
                vModelTop = await _operations.GetTopRegionCovid();

                return View(vModelTop);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReportByRegion(string iso)
        {
            try
            {
                TopProvinceViewModel vModelTop = new TopProvinceViewModel();
                vModelTop = await _operations.GetTopProvinceCovid(iso);

                return View(vModelTop);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
