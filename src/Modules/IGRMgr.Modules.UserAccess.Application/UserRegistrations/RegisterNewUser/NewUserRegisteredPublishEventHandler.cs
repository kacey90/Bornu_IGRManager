using IGRMgr.Modules.UserAccess.IntegrationEvents;
using MediatR;
using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class NewUserRegisteredPublishEventHandler : INotificationHandler<NewUserRegisteredNotification>
    {
        private readonly IEventBus _eventsBus;

        public NewUserRegisteredPublishEventHandler(IEventBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new NewUserRegisteredIntegrationEvent(notification.Id, notification.DomainEvent.OccurredOn,
                notification.DomainEvent.UserRegistrationId.Value,
                notification.DomainEvent.Email,
                notification.DomainEvent.FirstName,
                notification.DomainEvent.LastName,
                notification.DomainEvent.MiddleName,
                notification.DomainEvent.Name,
                string.Empty));

            return Task.CompletedTask;
        }
    }
}
