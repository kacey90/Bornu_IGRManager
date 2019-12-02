using Dapper;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using Nubalance.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.UserRegistrations
{
    public class UsersCounter : IUsersCounter
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UsersCounter(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public int ExistingUsersCount(string email)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM [users].[UserRegistrations] AS [User]" +
                               "WHERE [User].[Email] = @email";

            return connection.QuerySingle<int>(sql,
                new
                {
                    email
                });
        }
    }
}
