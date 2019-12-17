using Nubalance.BuildingBlocks.Domain;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners.Events
{
    public class BusinessPartnerActivatedEvent : DomainEventBase
    {
        public BusinessPartnerId Id { get; }

        public BusinessPartnerActivatedEvent(BusinessPartnerId id)
        {
            this.Id = id;
        }
    }
}