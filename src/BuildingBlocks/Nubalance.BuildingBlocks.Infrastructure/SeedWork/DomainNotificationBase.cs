using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Infrastructure.SeedWork
{
    public class DomainNotificationBase<T> : IDomainEventNotification<T> where T : IDomainEvent
    {
        public T DomainEvent { get; }

        public Guid Id  { get; }

        public DomainNotificationBase(T domainEvent)
        {
            this.Id = Guid.NewGuid();
            this.DomainEvent = domainEvent;
        }
    }
}
