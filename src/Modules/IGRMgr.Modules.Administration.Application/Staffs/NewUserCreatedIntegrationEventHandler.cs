using IGRMgr.Modules.Administration.Application.Configuration.Processing.InternalCommands;
using IGRMgr.Modules.UserAccess.IntegrationEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Staffs
{
    public class NewUserCreatedIntegrationEventHandler : INotificationHandler<NewUserCreatedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public NewUserCreatedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public Task Handle(NewUserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            _commandsScheduler.EnqueueAsync(new CreateStaffCommand(Guid.NewGuid(), notification.UserId, notification.FirstName,
                notification.LastName, notification.MiddleName, notification.Email, notification.PhoneNumber, string.Empty, notification.Gender,
                null, string.Empty));

            return Task.CompletedTask;
        }
    }
}
