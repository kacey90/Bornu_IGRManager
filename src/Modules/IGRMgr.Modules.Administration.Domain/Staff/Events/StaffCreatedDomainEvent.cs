using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.Staffs.Events
{
    public class StaffCreatedDomainEvent : DomainEventBase
    {
        public StaffId StaffId { get; }
        public StaffCreatedDomainEvent(StaffId staffId)
        {
            StaffId = staffId;
        }
    }
}
