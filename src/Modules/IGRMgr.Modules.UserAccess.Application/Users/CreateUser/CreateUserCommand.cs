using IGRMgr.Modules.UserAccess.Application.Configuration.Processing.InternalCommands;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.CreateUser
{
    internal class CreateUserCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal CreateUserCommand(Guid id, UserRegistrationId userRegistrationId,
            string email, string firstName, string middleName, string name, string gender, string phoneNumber,
            string role, bool isActive) : base(id)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
            FirstName = firstName;
            MiddleName = middleName;
            Name = name;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Role = role;
            IsActive = isActive;
        }

        public UserRegistrationId UserRegistrationId { get; }

        internal string Email { get; }

        internal string FirstName { get; }

        internal string LastName { get; }

        internal string MiddleName { get; }

        internal string Name { get; }

        internal string Gender { get; }

        internal string PhoneNumber { get; }

        internal bool IsActive { get; }

        internal string Role { get; }
    }
}
