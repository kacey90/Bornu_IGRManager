using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners
{
    public interface IBusinessPartnerRepository
    {
        Task AddAsync(BusinessPartner businessPartner);
        Task<BusinessPartner> GetByIdAsync(BusinessPartnerId businessPartnerId);
    }
}
