using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsAccessor
    {
        List<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
