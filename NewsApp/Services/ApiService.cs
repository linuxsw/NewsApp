using NewsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace NewsApp.Services
{
    public class ApiService
    {
        public async Task<Root> GetNews(string categoryName)
        {

            //-- For Proxy Usage
            var proxy = new WebProxy
            {
                Address = new Uri("http://proxy-chain.intel.com:912"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,
            };

            var httpClientHandler = new HttpClientHandler
            {
                Proxy = proxy,
            };

            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            //-- For Proxy Usage
            //var httpClient = new HttpClient();
            var httpClient = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            var response = await httpClient.GetStringAsync("https://gnews.io/api/v4/top-headlines?apikey=4ff6a244daf02b5b67525e6daae3f498&lang=en&country=us&category=" + categoryName.ToLower());

            return JsonConvert.DeserializeObject<Root>(response);

        }
    }
}
