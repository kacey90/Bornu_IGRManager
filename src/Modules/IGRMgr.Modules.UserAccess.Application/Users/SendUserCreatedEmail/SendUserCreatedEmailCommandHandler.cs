using IGRMgr.Modules.UserAccess.Application.Configuration.Processing;
using MediatR;
using Nubalance.BuildingBlocks.Infrastructure.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.SendUserCreatedEmail
{
    internal class SendUserCreatedEmailCommandHandler : ICommandHandler<SendUserCreatedEmailCommand>
    {
        private IEmailSender _emailSender;

        public SendUserCreatedEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task<Unit> Handle(SendUserCreatedEmailCommand request, CancellationToken cancellationToken)
        {
            var messageContent = $"Dear {request.Name}, {Environment.NewLine} {Environment.NewLine} An account has been " +
                $"created on the revenue platform with which you will have privileged access under the role of {request.Role}. " +
                $"Your user credentials are as follows; {Environment.NewLine} {Environment.NewLine} Username: {request.Email} " +
                $"{Environment.NewLine} Password: {request.Password} {Environment.NewLine} {Environment.NewLine} " +
                $"You will be prompted to change your password on the first logon. {Environment.NewLine} {Environment.NewLine} " +
                $"The Team.";

            var emailMassage = new EmailMessage(request.Email, "IGR Manager - Your Account has been Created",
                messageContent);

            _emailSender.SendEmail(emailMassage);

            return Unit.Task;
        }
    }
}
