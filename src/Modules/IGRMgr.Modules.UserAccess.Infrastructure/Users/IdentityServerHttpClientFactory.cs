using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Infrastructure.Users
{
    public class IdentityServerHttpClientFactory
    {
        public IdentityServerHttpClientFactory(HttpClient client)
        {
            HttpClient = client;
        }

        public HttpClient HttpClient { get; }
    }
}
