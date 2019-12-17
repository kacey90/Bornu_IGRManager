using IGRMgr.Modules.Administration.IntegrationEvents;
using IGRMgr.Modules.UserAccess.Application.Configuration.Processing.InternalCommands;
using IGRMgr.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.UserRegistrations.IntegratedEvents
{
    internal class BusinessPartnerCreatedIntegrationEventHandler : INotificationHandler<BusinessPartnerCreatedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        internal BusinessPartnerCreatedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public Task Handle(BusinessPartnerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            _commandsScheduler.EnqueueAsync(new RegisterNewUserCommand(
                notification.Email,
                notification.FirstName,
                notification.LastName,
                string.Empty,
                notification.PhoneNumber,
                string.Empty,
                notification.Role));

            return Task.CompletedTask;
        }
    }
}
