using Dapper;
using IGRMgr.Modules.Administration.Domain.Staffs;
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
    internal class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetStaffByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<StaffDto> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
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
                "FROM [administration].[Staffs] AS [Staff] WHERE [Staff].[Id] = @StaffId";

            var staff = await connection.QuerySingleAsync<StaffDto>(sql, new { StaffId = request.Id });
            return staff;
        }
    }
}
