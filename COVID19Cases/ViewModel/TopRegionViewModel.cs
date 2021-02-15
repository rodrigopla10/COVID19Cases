using COVID19Cases.Helper;
using COVID19Cases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19Cases.ViewModel
{
    public class TopRegionViewModel
    {

        public IEnumerable<TopRegion> topRegions { get; set; }
        public string iso { get; set; }
        public IEnumerable<RegionResponse.Region> regions { get; set; }
    }
}
