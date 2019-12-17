using Nubalance.BuildingBlocks.Domain;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners.Rules
{
    public class BusinessPartnerCannotBeDeactivatedTwice : IBusinessRule
    {
        private bool IsActive { get; }

        public BusinessPartnerCannotBeDeactivatedTwice(bool isActive)
        {
            IsActive = isActive;
        }

        public string Message => "The business partner is already deactivated";

        public bool IsBroken() => IsActive == false;
    }
}