using IGRMgr.Modules.Administration.IntegrationEvents;
using MediatR;
using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners
{
    internal class BusinessPartnerCreatedPublishEventNotificationHandler : INotificationHandler<BusinessPartnerCreatedNotification>
    {
        private readonly IEventBus _eventBus;

        internal BusinessPartnerCreatedPublishEventNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(BusinessPartnerCreatedNotification notification, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new BusinessPartnerCreatedIntegrationEvent(Guid.NewGuid(),
                notification.DomainEvent.FirstName,
                notification.DomainEvent.LastName,
                notification.DomainEvent.FullName,
                notification.DomainEvent.PhoneNumber,
                "BusinessPartner",
                notification.DomainEvent.OccurredOn));

            return Task.CompletedTask;
        }
    }
}
