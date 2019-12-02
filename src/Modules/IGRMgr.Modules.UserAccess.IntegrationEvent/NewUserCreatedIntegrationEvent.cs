using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.IntegrationEvents
{
    public class NewUserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string MiddleName { get; }

        public string Gender { get; }

        public string PhoneNumber { get; }

        public string Role { get; }

        public NewUserCreatedIntegrationEvent(Guid id, DateTime occurredOn, Guid userId, string email,
            string firstName, string lastName, string middleName, string gender, string phoneNumber, string role) : base(id, occurredOn)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Role = role;
        }
    }
}
