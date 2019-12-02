using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Domain.UserRegistrations.Events
{
    public class NewUserRegisteredDomainEvent : DomainEventBase
    {
        public UserRegistrationId UserRegistrationId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }
        public string Name { get; }
        public DateTime DateRegistered { get; }

        public NewUserRegisteredDomainEvent(UserRegistrationId userRegistrationId, string email, string firstName, string lastName,
            string middleName, string name, DateTime dateRegistered)
        {
            this.UserRegistrationId = userRegistrationId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Name = name;
            DateRegistered = dateRegistered;
        }
    }
}
