using IGRMgr.Modules.UserAccess.Application.Configuration.Processing;
using MediatR;
using Nubalance.BuildingBlocks.Infrastructure.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    internal class SendUserRegistrationConfirmationEmailCommandHandler : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
    {
        private IEmailSender _emailSender;

        public SendUserRegistrationConfirmationEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task<Unit> Handle(SendUserRegistrationConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage(request.Email, "MyMeetings - Please confirm your registration",
                "This should be link to confirmation page. For now, please execute HTTP request " +
                $"PATCH http://localhost:5000/userAccess/userRegistrations/{request.UserRegistrationId.Value}/confirm");

            _emailSender.SendEmail(emailMessage);

            return Unit.Task;
        }
    }
}
