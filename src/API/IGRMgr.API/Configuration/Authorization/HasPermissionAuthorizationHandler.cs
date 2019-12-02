using IGRMgr.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using IGRMgr.Modules.UserAccess.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Nubalance.BuildingBlocks.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.API.Configuration.Authorization
{
    internal class HasPermissionAuthorizationHandler : AttributeAuthorizationHandler<HasPermissionAuthorizationRequirement, HasPermissionAttribute>
    {
        private readonly IUserAccessModule _userAccessModule;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        public HasPermissionAuthorizationHandler(
            IExecutionContextAccessor executionContextAccessor,
            IUserAccessModule userAccessModule)
        {
            _executionContextAccessor = executionContextAccessor;
            _userAccessModule = userAccessModule;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionAuthorizationRequirement requirement, IEnumerable<HasPermissionAttribute> attributes)
        {
            var permissions = await _userAccessModule.ExecuteQueryAsync(new GetUserPermissionsQuery(_executionContextAccessor.UserId));
            foreach (var permissionAttribute in attributes)
            {
                if (!await AuthorizeAsync(permissionAttribute.Name, permissions))
                {
                    context.Fail();
                    return;
                }
            }

            context.Succeed(requirement);
        }

        private Task<bool> AuthorizeAsync(string permission, List<UserPermissionDto> permissions)
        {
#if !DEBUG
            return Task.FromResult(true);
#endif

            if (permissions.Any(x => x.Code == permission))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
