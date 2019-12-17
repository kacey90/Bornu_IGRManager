using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.HttpClients
{
    internal class SAPHttpClient
    {
        internal SAPHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        internal HttpClient HttpClient { get; }
    }
}
