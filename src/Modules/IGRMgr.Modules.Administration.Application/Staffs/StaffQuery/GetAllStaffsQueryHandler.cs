using Dapper;
using MediatR;
using Nubalance.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Staffs.StaffQuery
{
    internal class GetAllStaffsQueryHandler : IRequestHandler<GetAllStaffsQuery, List<StaffDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetAllStaffsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<StaffDto>> Handle(GetAllStaffsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT [Staff].[Id], " +
                "[Staff].[FirstName], " +
                "[Staff].[LastName], " +
                "[Staff].[MiddleName], " +
                "[Staff].[FullName], " +
                "[Staff].[EmailAddress], " +
                "[Staff].[PhoneNumber], " +
                "[Staff].[StaffNumber], " +
                "[Staff].[Gender], " +
                "[Staff].[DateOfBirth], " +
                "[Staff].[JobTitle], " +
                "[Staff].[IsActive], " +
                "[Staff].[CreateDate]" +
                "FROM [administration].[Staffs] AS [Staff]";

            var staffs = await connection.QueryAsync<StaffDto>(sql);

            return staffs.AsList();
        }
    }
}
