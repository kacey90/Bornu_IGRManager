using IGRMgr.Modules.Administration.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners.Queries
{
    public class GetBusinessPartnerByIdQuery : IQuery<BusinessPartnerDto>
    {
        public GetBusinessPartnerByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
