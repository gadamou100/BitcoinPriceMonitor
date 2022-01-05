using BitcoinPriceMonitor.Application.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastrucutre.Network
{
    public class HttpClientWrapper : IHttpGetter
    {
        public async Task<string> Get(string url, IDictionary<string, string>? headerParams = null)
        {
            var client = new RestClient(url);
            var request = new RestRequest("",DataFormat.Json);
            if(headerParams != null)
            {
                foreach (var item in headerParams)
                    request.AddHeader(item.Key, item.Value);
            }
            var result = await client.GetAsync<string>(request);
            return result;

        }
    }
}
