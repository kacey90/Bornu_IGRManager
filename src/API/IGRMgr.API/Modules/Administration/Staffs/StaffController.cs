using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGRMgr.API.Configuration.Authorization;
using IGRMgr.Modules.Administration.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IGRMgr.API.Modules.Administration.Staffs
{
    [Route("api/administration/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IAdministrationModule _adminModule;

        public StaffController(IAdministrationModule administrationModule)
        {
            _adminModule = administrationModule;
        }

        [HttpPost]
        [HasPermission(AdministrationPermissions.CreateStaff)]
        public async Task<IActionResult> CreateStaff()
        {
            await _adminModule.ExecuteCommandAsync(new CreateUserCo)
        }
    }
}