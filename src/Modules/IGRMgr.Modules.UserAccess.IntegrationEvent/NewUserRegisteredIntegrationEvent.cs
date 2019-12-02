using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.IntegrationEvents
{
    public class NewUserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string MiddleName { get; }

        public string Name { get; }

        public string Role { get; }
        public NewUserRegisteredIntegrationEvent(Guid id, DateTime occurredOn, Guid userId, string email,
            string firstName, string lastName, string middleName, string name, string role) : base(id, occurredOn)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Name = name;
            Role = role;
        }
    }
}
