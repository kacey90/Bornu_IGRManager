using Autofac;
using Autofac.Extensions.DependencyInjection;
using IGRMgr.Modules.UserAccess.Application.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nubalance.BuildingBlocks.Application;
using Nubalance.BuildingBlocks.Infrastructure.Emails;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.API
{
    internal static class ServicesAutofacExtensions
    {
        internal static void RegisterServicesOnAutofac(
            ContainerBuilder builder,
            string migrationsAssembly,
            IConfiguration configuration,
            ILogger logger)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<ExecutionContextAccessor>().As<IExecutionContextAccessor>().SingleInstance();

            ExecutionContextAccessor executionContextAccessor = null;
            var emailsConfiguration = new EmailsConfiguration(configuration["EmailsConfiguration:FromEmail"]);

            builder.Register(e => new ExecutionContextAccessor(e.Resolve<IHttpContextAccessor>()));
            builder.Register(container =>
            {
                executionContextAccessor = new ExecutionContextAccessor(container.Resolve<IHttpContextAccessor>());
                return executionContextAccessor;
            });
            

            UserAccessStartup.Initialize(
                configuration.GetConnectionString("DefaultConnection"),
                executionContextAccessor,
                migrationsAssembly,
                logger,
                emailsConfiguration,
                configuration["Security:TextEncryptionKey"]);

        }
    }
}
