using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGRMgr.API.Configuration.Authorization;
using IGRMgr.Modules.UserAccess.Application.Contracts;
using IGRMgr.Modules.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;
using IGRMgr.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IGRMgr.API.Modules.Administration.UserAccess
{
    [Route("api/userAccess/[controller]")]
    [ApiController]
    public class UserRegistrationsController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;

        public UserRegistrationsController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [HttpPost]
        [HasPermission(AdministrationPermissions.CreateStaff)]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserRequest request)
        {
            await _userAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(request.Email, request.FirstName,
                request.LastName, request.MiddleName, request.PhoneNumber, request.Gender, request.Role));

            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("{userRegistrationId}/confirm")]
        public async Task<IActionResult> ConfirmRegistration(Guid userRegistrationId)
        {
            await _userAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(userRegistrationId));

            return Ok();
        }
    }
}