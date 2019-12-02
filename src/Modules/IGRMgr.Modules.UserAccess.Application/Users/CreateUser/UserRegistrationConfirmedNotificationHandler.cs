using IGRMgr.Modules.UserAccess.Application.Configuration.Processing.InternalCommands;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.CreateUser
{
    public class UserRegistrationConfirmedNotificationHandler : INotificationHandler<UserRegistrationConfirmedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public UserRegistrationConfirmedNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public Task Handle(UserRegistrationConfirmedNotification notification, CancellationToken cancellationToken)
        {
            _commandsScheduler.EnqueueAsync(new CreateUserCommand(Guid.NewGuid(), notification.DomainEvent.UserRegistrationId, 
                notification.DomainEvent.Email, notification.DomainEvent.FirstName, 
                notification.DomainEvent.MiddleName, notification.DomainEvent.Name,
                notification.DomainEvent.Gender, notification.DomainEvent.PhoneNumber, 
                notification.DomainEvent.Role, notification.DomainEvent.IsActive));

            return Task.CompletedTask;
        }
    }
}
