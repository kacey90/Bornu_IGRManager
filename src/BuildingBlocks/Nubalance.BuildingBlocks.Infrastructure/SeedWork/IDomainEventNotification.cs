using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Infrastructure.SeedWork
{
    public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
    {
        TEventType DomainEvent { get; }
    }

    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }
}
