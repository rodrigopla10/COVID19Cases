using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static COVID19Cases.Helper.RegionResponse;

namespace COVID19Cases.Helper
{
    public class ReportResponse
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public class Report
        {
            public string date { get; set; }
            public int confirmed { get; set; }
            public int deaths { get; set; }
            public int recovered { get; set; }
            public int confirmed_diff { get; set; }
            public int deaths_diff { get; set; }
            public int recovered_diff { get; set; }
            public string last_update { get; set; }
            public int active { get; set; }
            public int active_diff { get; set; }
            public double fatality_rate { get; set; }
            public Region region { get; set; }
        }

        public class Root
        {
            public List<Report> data { get; set; }
        }
    }
}
