using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners.Rules
{
    public class BusinessPartnerMustBeUniqueRule : IBusinessRule
    {
        private readonly IBusinessPartnersCounter _businessPartnersCounter;
        private readonly string _email;

        public BusinessPartnerMustBeUniqueRule(IBusinessPartnersCounter businessPartnersCounter, string email)
        {
            _businessPartnersCounter = businessPartnersCounter;
            _email = email;
        }
        public string Message => $"Business partner - {_email} already exists.";

        public bool IsBroken() => _businessPartnersCounter.CountBusinessPartnerWithEmail(_email) > 0;
    }
}
