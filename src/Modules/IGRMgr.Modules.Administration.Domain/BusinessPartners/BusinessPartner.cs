using IGRMgr.Modules.Administration.Domain.BusinessPartners.Events;
using IGRMgr.Modules.Administration.Domain.BusinessPartners.Rules;
using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners
{
    public class BusinessPartner : Entity, IAggregateRoot
    {
        public BusinessPartnerId Id { get; private set; }

        private string _firstName;

        private string _lastName;

        private string _name;

        private string _phoneNumber;

        private string _email;

        private BusinessPartnerLocation _location;

        private bool _isActive;

        private DateTime _createDate;

        private DateTime? _updateDate;

        private BusinessPartner()
        {

        }

        internal BusinessPartner(Guid id, 
            string firstName, 
            string lastName, 
            string phoneNumber, 
            string email, 
            BusinessPartnerLocation location,
            IBusinessPartnersCounter businessPartnersCounter)
        {
            this.CheckRule(new BusinessPartnerMustBeUniqueRule(businessPartnersCounter, _email));

            Id = new BusinessPartnerId(id);
            _firstName = firstName;
            _lastName = lastName;
            _name = $"{firstName} {lastName}";
            _phoneNumber = phoneNumber;
            _email = email;
            _location = location;
            _isActive = true;
            _createDate = DateTime.Now;

            this.AddDomainEvent(new BusinessPartnerCreatedEvent(Id, _firstName, _lastName, _name, _phoneNumber, _email));
        }

        public static BusinessPartner Create(Guid id, 
            string firstName, 
            string lastName, 
            string phoneNumber, 
            string email,
            string street,
            string city,
            string postalCode,
            string state,
            IBusinessPartnersCounter businessPartnersCounter)
        {
            return new BusinessPartner(id, firstName, lastName, phoneNumber, email,
                new BusinessPartnerLocation(street, city, postalCode, state), businessPartnersCounter);
        }

        public void Activate()
        {
            this.CheckRule(new BusinessPartnerCannotBeActivatedTwice(_isActive));

            _isActive = true;
            _updateDate = DateTime.Now;

            this.AddDomainEvent(new BusinessPartnerActivatedEvent(this.Id));
        }

        public void Deactivate()
        {
            this.CheckRule(new BusinessPartnerCannotBeDeactivatedTwice(_isActive));

            _isActive = false;
            _updateDate = DateTime.Now;

            this.AddDomainEvent(new BusinessPartnerDeactivatedEvent(this.Id));
        }
    }
}
