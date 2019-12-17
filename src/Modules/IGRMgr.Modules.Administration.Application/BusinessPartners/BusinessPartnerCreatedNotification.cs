using IGRMgr.Modules.Administration.Domain.BusinessPartners.Events;
using Newtonsoft.Json;
using Nubalance.BuildingBlocks.Infrastructure.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners
{
    public class BusinessPartnerCreatedNotification : DomainNotificationBase<BusinessPartnerCreatedEvent>
    {
        [JsonConstructor]
        public BusinessPartnerCreatedNotification(BusinessPartnerCreatedEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
