using Autofac;
using IGRMgr.Modules.UserAccess.Application.Configuration.DataAccess;
using IGRMgr.Modules.UserAccess.Application.Configuration.Domain;
using IGRMgr.Modules.UserAccess.Application.Configuration.Email;
using IGRMgr.Modules.UserAccess.Application.Configuration.EventBus;
using IGRMgr.Modules.UserAccess.Application.Configuration.Logging;
using IGRMgr.Modules.UserAccess.Application.Configuration.Mediation;
using IGRMgr.Modules.UserAccess.Application.Configuration.Processing;
using IGRMgr.Modules.UserAccess.Application.Configuration.Processing.Outbox;
using IGRMgr.Modules.UserAccess.Application.Configuration.Quartz;
using IGRMgr.Modules.UserAccess.Application.Configuration.Security;
using Nubalance.BuildingBlocks.Application;
using Nubalance.BuildingBlocks.Infrastructure.Emails;
using Serilog;
using Serilog.Extensions.Logging;
//using Serilog.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Configuration
{
    public class UserAccessStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString,
            //IExecutionContextAccessor executionContextAccessor,
            string migrationsAssembly,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey)
        {
            var moduleLogger = logger.ForContext("Module", "UserAccess");

            ConfigureCompositionRoot(connectionString,
                migrationsAssembly,
                logger,
                emailsConfiguration,
                textEncryptionKey);

            QuartzStartup.Initialize(moduleLogger);

            EventBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            string migrationsAssembly,
            //IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "UserAccess")));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, migrationsAssembly, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventBusModule());
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new EmailModule(emailsConfiguration));
            containerBuilder.RegisterModule(new SecurityModule(textEncryptionKey));

            //containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            UserAccessCompositionRoot.SetContainer(_container);
            
        }
    }
}
