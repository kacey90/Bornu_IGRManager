using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nubalance.BuildingBlocks.HttpClientFactory
{
    public class ExternalHttpClient
    {
        public ExternalHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }
    }
}
