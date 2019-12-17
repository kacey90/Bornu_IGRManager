using Dapper;
using IGRMgr.Modules.Administration.Domain.BusinessPartners;
using Nubalance.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners
{
    public class BusinessPartnersCounter : IBusinessPartnersCounter
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public BusinessPartnersCounter(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public int CountBusinessPartnerWithEmail(string email)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT COUNT(*) " +
                "FROM [administration].[BusinessPartners] " +
                "AS [BusinessPartner] " +
                "WHERE [BusinessPartner].[EmailAddress] = @emailAddress";

            return connection.QuerySingle<int>(sql, new { emailAddress = email });
        }
    }
}
