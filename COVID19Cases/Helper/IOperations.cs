using COVID19Cases.Models;
using COVID19Cases.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19Cases.Helper
{
    public interface IOperations
    {
        Task<TopRegionViewModel> GetTopRegionCovid();

        Task<TopProvinceViewModel> GetTopProvinceCovid(string iso);
    }
}
