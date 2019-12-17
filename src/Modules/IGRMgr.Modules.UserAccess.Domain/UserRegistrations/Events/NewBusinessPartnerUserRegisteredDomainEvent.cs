using System;
using Nubalance.BuildingBlocks.Domain;

namespace IGRMgr.Modules.UserAccess.Domain.UserRegistrations
{
    public class NewBusinessPartnerUserRegisteredDomainEvent : DomainEventBase
    {
        public UserRegistrationId UserRegistrationId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Name { get; }
        public DateTime DateRegistered { get; }

        public NewBusinessPartnerUserRegisteredDomainEvent(UserRegistrationId id, 
            string email, 
            string firstName, 
            string lastName, 
            string name, 
            DateTime dateRegistered)
        {
            this.UserRegistrationId = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
            DateRegistered = dateRegistered;
        }
    }
}