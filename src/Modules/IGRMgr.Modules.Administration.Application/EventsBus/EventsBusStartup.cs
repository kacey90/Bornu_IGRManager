using Autofac;
using IGRMgr.Modules.Administration.Application.Configuration;
using IGRMgr.Modules.UserAccess.IntegrationEvents;
using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.EventsBus
{
    internal static class EventsBusStartup
    {
        internal static void Initialize(
            ILogger logger)
        {
            SubscribeToIntegrationEvents(logger);
        }

        private static void SubscribeToIntegrationEvents(ILogger logger)
        {
            var eventBus = AdministrationCompositionRoot.BeginLifetimeScope().Resolve<IEventBus>();

            SubscribeToIntegrationEvent<NewUserRegisteredIntegrationEvent>(eventBus, logger);
            SubscribeToIntegrationEvent<NewUserCreatedIntegrationEvent>(eventBus, logger);
        }

        private static void SubscribeToIntegrationEvent<T>(IEventBus eventBus, ILogger logger) where T : IntegrationEvent
        {
            logger.Information("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
            eventBus.Subscribe(
                new IntegrationEventGenericHandler<T>());
        }
    }
}
