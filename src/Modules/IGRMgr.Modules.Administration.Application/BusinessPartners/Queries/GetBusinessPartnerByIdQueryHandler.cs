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
    internal class GetBusinessPartnerByIdQueryHandler : IRequestHandler<GetBusinessPartnerByIdQuery, BusinessPartnerDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetBusinessPartnerByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<BusinessPartnerDto> Handle(GetBusinessPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT * FROM [administration].[BusinessPartners] AS [BusinessPartner] " +
                "WHERE [BusinessPartner].[Id] = @BusinessPartnerId";

            var businessPartner = await connection.QuerySingleAsync<BusinessPartnerDto>(sql, new { BusinessPartnerId = request.Id });
            return businessPartner;
        }
    }
}
