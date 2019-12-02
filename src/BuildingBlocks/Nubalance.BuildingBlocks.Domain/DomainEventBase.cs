using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Domain
{
    public class DomainEventBase : IDomainEvent
    {
        public DateTime OccurredOn { get; set; }

        public DomainEventBase()
        {
            this.OccurredOn = DateTime.UtcNow;
        }
    }
}
