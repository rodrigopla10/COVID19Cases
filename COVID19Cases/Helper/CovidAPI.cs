using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace COVID19Cases.Helper
{
    public class CovidAPI
    {
        private readonly IConfiguration configuration;

        public CovidAPI()
        {
            configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        }

        public HttpRequestMessage ApiRegionRequest()
        {

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/regions"),
            };

            request.Headers.Add("x-rapidapi-key", configuration.GetSection("x-rapidapi-key").Value);
            request.Headers.Add("x-rapidapi-host", configuration.GetSection("x-rapidapi-host").Value);

            return request;
        }

        public HttpRequestMessage ApiReportRegRequest()
        {
            var today= DateTime.Today;
            var yesterday = today.AddDays(-1);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://covid-19-statistics.p.rapidapi.com/reports?date={yesterday.ToString("yyyy-MM-dd")}"),
            };

            request.Headers.Add("x-rapidapi-key", configuration.GetSection("x-rapidapi-key").Value);
            request.Headers.Add("x-rapidapi-host", configuration.GetSection("x-rapidapi-host").Value);

            return request;
        }

        public HttpRequestMessage ApiReportProvRequest(string iso)
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://covid-19-statistics.p.rapidapi.com/reports?iso={iso}&date={yesterday.ToString("yyyy-MM-dd")}"),
            };

            request.Headers.Add("x-rapidapi-key", configuration.GetSection("x-rapidapi-key").Value);
            request.Headers.Add("x-rapidapi-host", configuration.GetSection("x-rapidapi-host").Value);

            return request;
        }

    }
}
