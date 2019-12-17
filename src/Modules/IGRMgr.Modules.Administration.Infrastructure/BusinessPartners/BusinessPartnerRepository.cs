using IGRMgr.Modules.Administration.Domain.BusinessPartners;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Infrastructure.BusinessPartners
{
    internal class BusinessPartnerRepository : IBusinessPartnerRepository
    {
        private readonly AdministrationContext _administrationContext;

        internal BusinessPartnerRepository(AdministrationContext administrationContext)
        {
            _administrationContext = administrationContext;
        }

        public async Task AddAsync(BusinessPartner businessPartner)
        {
            await _administrationContext.BusinessPartners.AddAsync(businessPartner);
        }

        public async Task<BusinessPartner> GetByIdAsync(BusinessPartnerId businessPartnerId)
        {
            return await _administrationContext.BusinessPartners.FirstOrDefaultAsync(x => x.Id == businessPartnerId);
        }
    }
}
