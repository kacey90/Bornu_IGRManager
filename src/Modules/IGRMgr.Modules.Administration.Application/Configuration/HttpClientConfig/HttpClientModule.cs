using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Configuration.HttpClientConfig
{
    internal class HttpClientModule<THttpClient> : Module
    {
        internal HttpClientModule(Action<HttpClient> clientConfigurator)
        {
            ClientConfigurator = clientConfigurator;
        }

        internal Action<HttpClient> ClientConfigurator { get; }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, 
            IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);

            if (registration.Activator.LimitType == typeof(THttpClient))
            {
                registration.Preparing += Registration_Preparing;
            }
        }

        private void Registration_Preparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(new[]
            {
                new ResolvedParameter(
                        (p, i) => p.ParameterType == typeof(HttpClient),
                        (p, i) => {
                            HttpClient client = i.Resolve<IHttpClientFactory>().CreateClient();
                            ClientConfigurator(client);
                            return client;
                        }
                    )
            });
        }
    }
}
