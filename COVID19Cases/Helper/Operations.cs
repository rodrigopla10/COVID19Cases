using COVID19Cases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace COVID19Cases.Helper
{
    public class Operations : IOperations
    {
        public async Task<List<TopRegion>> GetTopRegionCovidAsync()
        {

            List<TopRegion> regions = new List<TopRegion>();
            var client = new HttpClient();
            Helper.CovidAPI api = new Helper.CovidAPI();

            try
            {
                HttpRequestMessage request = api.ApiRegionRequest();

                using (var response1 = await client.SendAsync(request))
                {
                    response1.EnsureSuccessStatusCode();
                    string apiResponseStr = await response1.Content.ReadAsStringAsync();

                    var desRegResponse = JsonConvert.DeserializeObject<RegionResponse.Root>(apiResponseStr.Replace(System.Environment.NewLine, string.Empty));

                    List<ReportResponse.Report> lstReportAll = new List<ReportResponse.Report>();

                    request = api.ApiReportRequest();

                    using (var response2 = await client.SendAsync(request))
                    {
                        response2.EnsureSuccessStatusCode();
                        apiResponseStr = await response2.Content.ReadAsStringAsync();
                        var desRepResponse = JsonConvert.DeserializeObject<ReportResponse.Root>(apiResponseStr.Replace(System.Environment.NewLine, string.Empty));
                        lstReportAll.AddRange(desRepResponse.data);
                    }

                    var result =(from r in lstReportAll
                                  group r by new { r.region.iso } into rr
                                  select new TopRegion
                                  {
                                      Region = rr.Key.iso,
                                      Cases = rr.Sum(s => s.confirmed),
                                      Deaths = rr.Sum(s => s.deaths)                                                                          
                                  }).OrderByDescending(s => s.Cases).Take(10);



                    regions = result.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return regions;
        }
    }
}
