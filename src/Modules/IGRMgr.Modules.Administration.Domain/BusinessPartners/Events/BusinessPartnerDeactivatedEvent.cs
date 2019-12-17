using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners.Events
{
    public class BusinessPartnerDeactivatedEvent : DomainEventBase
    {
        public BusinessPartnerId Id { get; }

        public BusinessPartnerDeactivatedEvent(BusinessPartnerId id)
        {
            this.Id = id;
        }
    }
}
