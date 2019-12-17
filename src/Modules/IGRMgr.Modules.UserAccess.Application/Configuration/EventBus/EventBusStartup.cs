using Autofac;
using IGRMgr.Modules.Administration.IntegrationEvents;
using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Configuration.EventBus
{
    public static class EventBusStartup
    {
        public static void Initialize(
            ILogger logger)
        {
            SubscribeToIntegrationEvents(logger);
        }

        private static void SubscribeToIntegrationEvents(ILogger logger)
        {
            var eventBus = UserAccessCompositionRoot.BeginLifetimeScope().Resolve<IEventBus>();

            SubscribeToIntegrationEvent<StaffCreatedIntegrationEvent>(eventBus, logger);
            SubscribeToIntegrationEvent<BusinessPartnerCreatedIntegrationEvent>(eventBus, logger);
        }

        private static void SubscribeToIntegrationEvent<T>(IEventBus eventBus, ILogger logger) where T : IntegrationEvent
        {
            logger.Information("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
            eventBus.Subscribe(
                new IntegrationEventGenericHandler<T>());
        }
    }
}
