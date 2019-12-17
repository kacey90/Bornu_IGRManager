using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.IntegrationEvents
{
    public class BusinessPartnerCreatedIntegrationEvent : IntegrationEvent
    {
        public BusinessPartnerCreatedIntegrationEvent(Guid id,
            string firstName,
            string lastName,
            string name,
            string phone,
            string role,
            DateTime occurredOn) : base(id, occurredOn)
        {
            FirstName = firstName;
            LastName = lastName;
            Name = name;
            PhoneNumber = phone;
            Role = role;
        }

        public string Email { get; }
        
        public string FirstName { get; }

        public string LastName { get; }

        public string Name { get; }

        public string PhoneNumber { get; }

        public string Role { get; }
    }
}
