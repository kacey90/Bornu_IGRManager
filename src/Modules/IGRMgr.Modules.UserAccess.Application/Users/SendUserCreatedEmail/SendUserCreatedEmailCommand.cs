using IGRMgr.Modules.UserAccess.Application.Configuration.Processing.InternalCommands;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using IGRMgr.Modules.UserAccess.Domain.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.SendUserCreatedEmail
{
    internal class SendUserCreatedEmailCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal SendUserCreatedEmailCommand(Guid id, UserId userRegistrationId, string email,
            string password, string name, string role) : base(id)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
            Password = password;
            Name = name;
            Role = role;
        }

        internal UserId UserRegistrationId { get; }

        internal string Email { get; }

        internal string Password { get; }

        internal string Name { get; }

        internal string Role { get; }
    }
}
