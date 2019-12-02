using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.IntegrationEvents
{
    public class StaffCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid StaffId { get; set; }
        public StaffCreatedIntegrationEvent(Guid id, DateTime occurredOn, Guid staffId) : base(id, occurredOn)
        {
            StaffId = staffId;
        }
    }
}
