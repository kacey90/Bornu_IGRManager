using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Infrastructure.EventBus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : IntegrationEvent;

        void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent;

        void StartConsuming();
    }
}
