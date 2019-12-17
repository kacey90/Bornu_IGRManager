using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners
{
    public class BusinessPartnerId : TypedIdValueBase
    {
        public BusinessPartnerId(Guid value) : base(value)
        {
        }
    }
}
