﻿using COVID19Cases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19Cases.Helper
{
    public interface IOperations
    {
        Task<List<TopRegion>> GetTopRegionCovidAsync();
    }
}
