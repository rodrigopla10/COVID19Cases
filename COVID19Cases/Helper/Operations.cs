using COVID19Cases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using COVID19Cases.ViewModel;

namespace COVID19Cases.Helper
{
    public class Operations : IOperations
    {
        public async Task<TopRegionViewModel> GetTopRegionCovidAsync()
        {

            TopRegionViewModel regVM = new TopRegionViewModel();
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

                    regVM.regions = desRegResponse.data;

                    List<ReportResponse.Report> lstReportAll = new List<ReportResponse.Report>();

                    request = api.ApiReportRequest();

                    using (var response2 = await client.SendAsync(request))
                    {
                        response2.EnsureSuccessStatusCode();
                        apiResponseStr = await response2.Content.ReadAsStringAsync();
                        var desRepResponse = JsonConvert.DeserializeObject<ReportResponse.Root>(apiResponseStr.Replace(System.Environment.NewLine, string.Empty));
                        lstReportAll.AddRange(desRepResponse.data);
                    }

                    var result = (from r in lstReportAll
                                  group r by new { r.region.name } into rr
                                  select new TopRegion
                                  {
                                      Region = rr.Key.name,
                                      Cases = rr.Sum(s => s.confirmed),
                                      Deaths = rr.Sum(s => s.deaths)
                                  }).OrderByDescending(s => s.Cases).Take(10);

                    regVM.topRegions = result.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return regVM;
        }
    }
}
