using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners
{
    public interface IBusinessPartnersCounter
    {
        int CountBusinessPartnerWithEmail(string email);
    }
}
