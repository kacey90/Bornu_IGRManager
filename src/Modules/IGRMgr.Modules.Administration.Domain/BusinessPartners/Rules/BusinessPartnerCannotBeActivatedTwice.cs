using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners.Rules
{
    public class BusinessPartnerCannotBeActivatedTwice : IBusinessRule
    {
        public BusinessPartnerCannotBeActivatedTwice(bool isActive)
        {
            IsActive = isActive;
        }
        private bool IsActive { get; }
        public string Message => "This Business partner is already active.";

        public bool IsBroken() => IsActive == true;
    }
}
