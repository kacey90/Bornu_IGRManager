using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nubalance.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
