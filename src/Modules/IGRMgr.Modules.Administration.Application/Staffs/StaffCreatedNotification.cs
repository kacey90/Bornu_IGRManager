using IGRMgr.Modules.Administration.Domain.Staffs.Events;
using Newtonsoft.Json;
using Nubalance.BuildingBlocks.Infrastructure.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace IGRMgr.Modules.Administration.Application.Staffs
{
    public class StaffCreatedNotification : DomainNotificationBase<StaffCreatedDomainEvent>
    {
        [JsonConstructor]
        public StaffCreatedNotification(StaffCreatedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
