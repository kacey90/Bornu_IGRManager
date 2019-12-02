using IGRMgr.Modules.Administration.Domain.Users;
using Nubalance.BuildingBlocks.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Users
{
    internal class UserContext : IUserContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public UserContext(IExecutionContextAccessor executionContextAccessor)
        {
            this._executionContextAccessor = executionContextAccessor;
        }
        public UserId UserId => new UserId(_executionContextAccessor.UserId);
    }
}
