using Dapper;
using MediatR;
using Nubalance.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners.Queries
{
    internal class GetAllBusinessPartnersQueryHandler : IRequestHandler<GetAllBusinessPartnersQuery, List<BusinessPartnerDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetAllBusinessPartnersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<BusinessPartnerDto>> Handle(GetAllBusinessPartnersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT * FROM [administration].[BusinessPartners]";

            var businessPartners = await connection.QueryAsync<BusinessPartnerDto>(sql);

            return businessPartners.AsList();
        }
    }
}
