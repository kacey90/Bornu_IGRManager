using IGRMgr.Modules.UserAccess.Application.Configuration.Processing.InternalCommands;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    internal class SendUserRegistrationConfirmationEmailCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal SendUserRegistrationConfirmationEmailCommand(Guid id, UserRegistrationId userRegistrationId, string email)
        : base(id)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
        }

        internal UserRegistrationId UserRegistrationId { get; }

        internal string Email { get; }
    }
}
