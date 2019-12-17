using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using IGRMgr.Modules.UserAccess.IntegrationEvents;
using MediatR;
using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.CreateUser
{
    public class NewUserCreatedPublishedEventHandler : INotificationHandler<UserCreatedNotification>
    {
        private readonly IEventBus _eventsBus;

        public NewUserCreatedPublishedEventHandler(IEventBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.DomainEvent.Role == nameof(UserRole.BusinessPartner))
                return Task.CompletedTask;

            _eventsBus.Publish(new NewUserCreatedIntegrationEvent(notification.Id, notification.DomainEvent.OccurredOn,
                notification.DomainEvent.UserId.Value,
                notification.DomainEvent.Email,
                notification.DomainEvent.FirstName,
                notification.DomainEvent.LastName,
                notification.DomainEvent.MiddleName,
                notification.DomainEvent.Gender,
                notification.DomainEvent.PhoneNumber,
                notification.DomainEvent.Role));

            return Task.CompletedTask;
        }
    }
}
