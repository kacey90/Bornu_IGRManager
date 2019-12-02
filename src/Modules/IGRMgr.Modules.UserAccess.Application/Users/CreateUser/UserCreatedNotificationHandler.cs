using IGRMgr.Modules.UserAccess.Application.Configuration.Processing.InternalCommands;
using IGRMgr.Modules.UserAccess.Application.Users.SendUserCreatedEmail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.CreateUser
{
    public class UserCreatedNotificationHandler : INotificationHandler<UserCreatedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public UserCreatedNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            _commandsScheduler.EnqueueAsync(new SendUserCreatedEmailCommand(Guid.NewGuid(),
                notification.DomainEvent.UserId, notification.DomainEvent.Email, notification.DomainEvent.Password,
                notification.DomainEvent.Name, notification.DomainEvent.Role));

            return Task.CompletedTask;
        }
    }
}
