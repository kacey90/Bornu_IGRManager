using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public UserCreatedDomainEvent(UserId userId, string email, string firstName, string lastName, string middleName,
            string password, string name, string role, 
            string gender, string phoneNumber)
        {
            UserId = userId;
            Email = email;
            Password = password;
            Name = name;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Role = role;
            Gender = gender;
            PhoneNumber = phoneNumber;
        }

        public UserId UserId { get; }
        
        public string Name { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string MiddleName { get; }

        public string Email { get; }

        public string Role { get; }

        public string Gender { get; }

        public string PhoneNumber { get; }

        public string Password { get; }
    }
}
