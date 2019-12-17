using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners.Events
{
    public class BusinessPartnerCreatedEvent : DomainEventBase
    {
        public BusinessPartnerCreatedEvent(BusinessPartnerId id,
            string firstName,
            string lastName,
            string fullname,
            string phoneNumber,
            string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullname;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        public BusinessPartnerId Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
    }
}
