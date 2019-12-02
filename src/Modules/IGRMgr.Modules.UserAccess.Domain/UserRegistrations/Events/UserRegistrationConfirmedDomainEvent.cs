using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Domain.UserRegistrations.Events
{
    public class UserRegistrationConfirmedDomainEvent : DomainEventBase
    {
        public UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId, 
            string firstName,
            string lastName,
            string middleName,
            string email,
            string name,
            string phoneNumber,
            string gender,
            bool isActive, 
            string role)
        {
            UserRegistrationId = userRegistrationId;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Name = name;
            IsActive = isActive;
            Role = role;
        }

        public UserRegistrationId UserRegistrationId { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string MiddleName { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string Gender { get; }

        public string Name { get; }

        public bool IsActive { get; }

        public string Role { get; }
    }
}
