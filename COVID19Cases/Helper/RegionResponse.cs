using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19Cases.Helper
{
    public class RegionResponse
    {
        public class Region
        {
            public string iso { get; set; }
            public string name { get; set; }
            public string province { get; set; }
            public string lat { get; set; }
            public string @long { get; set; }
            public List<object> cities { get; set; }
        }

        public class Root
        {
            public List<Region> data { get; set; }
        }

    }
}
