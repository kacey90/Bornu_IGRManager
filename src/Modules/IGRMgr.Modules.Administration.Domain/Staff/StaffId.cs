using System;
using Nubalance.BuildingBlocks.Domain;

namespace IGRMgr.Modules.Administration.Domain.Staffs
{
    public class StaffId : TypedIdValueBase
    {
        public StaffId(Guid value) : base(value)
        {
        }
    }
}