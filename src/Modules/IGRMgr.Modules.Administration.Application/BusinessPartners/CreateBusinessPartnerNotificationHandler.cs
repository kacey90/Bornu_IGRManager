using IGRMgr.Modules.Administration.Application.Configuration.Processing.InternalCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners
{
    internal class CreateBusinessPartnerNotificationHandler : INotificationHandler<CreateBusinessPartnerNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        internal CreateBusinessPartnerNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(CreateBusinessPartnerNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new CreateSAPBusinessPartnerCommand(Guid.NewGuid(), notification.DomainEvent.FirstName,
                notification.DomainEvent.LastName,
                notification.DomainEvent.PhoneNumber,
                notification.DomainEvent.Email));
        }
    }
}
