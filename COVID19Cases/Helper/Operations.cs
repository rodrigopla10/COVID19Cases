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
        private readonly HttpClient client = new HttpClient();
        private readonly Helper.CovidAPI api = new Helper.CovidAPI();

        public async Task<TopRegionViewModel> GetTopRegionCovid()
        {

            TopRegionViewModel regVM = new TopRegionViewModel();

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

                    request = api.ApiReportRegRequest();

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

        public async Task<TopProvinceViewModel> GetTopProvinceCovid(string iso)
        {
            TopProvinceViewModel provVM = new TopProvinceViewModel();

            List<ReportResponse.Report> lstReportAll = new List<ReportResponse.Report>();
            HttpRequestMessage request = api.ApiReportProvRequest(iso);

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string apiResponseStr = await response.Content.ReadAsStringAsync();
                var desRepResponse = JsonConvert.DeserializeObject<ReportResponse.Root>(apiResponseStr.Replace(System.Environment.NewLine, string.Empty));
                lstReportAll.AddRange(desRepResponse.data);
            }

            var result = (from p in lstReportAll
                          group p by new { p.region.province } into rr
                          select new TopProvince
                          {
                              Province = rr.Key.province,
                              Cases = rr.Sum(s => s.confirmed),
                              Deaths = rr.Sum(s => s.deaths)
                          }).OrderByDescending(s => s.Cases).Take(10);

            provVM.topProvinces = result.ToList();

            return provVM;
        }
    }
}
