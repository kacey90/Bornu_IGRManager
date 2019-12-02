using Autofac;
using IGRMgr.Modules.Administration.Application.Configuration.Authentication;
using IGRMgr.Modules.Administration.Application.Configuration.DataAccess;
using IGRMgr.Modules.Administration.Application.Configuration.Logging;
using IGRMgr.Modules.Administration.Application.Configuration.Mediation;
using IGRMgr.Modules.Administration.Application.Configuration.Processing;
using IGRMgr.Modules.Administration.Application.Configuration.Processing.Outbox;
using IGRMgr.Modules.Administration.Application.Configuration.Quartz;
using IGRMgr.Modules.Administration.Application.EventsBus;
using Nubalance.BuildingBlocks.Application;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Configuration
{
    public class AdministrationStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            //IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "Administration");

            ConfigureContainer(connectionString, moduleLogger);

            QuartzStartup.Initialize(moduleLogger);

            EventsBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureContainer(string connectionString,
            ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));

            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule());
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new AuthenticationModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new QuartzModule());

            //containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            AdministrationCompositionRoot.SetContainer(_container);
        }
    } 
}
