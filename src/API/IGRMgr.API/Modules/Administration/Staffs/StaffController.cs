using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGRMgr.API.Configuration.Authorization;
using IGRMgr.Modules.Administration.Application.Contracts;
using IGRMgr.Modules.Administration.Application.Staffs.StaffQuery;
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

        [HttpGet]
        [HasPermission(AdministrationPermissions.GetAllStaffList)]
        public async Task<IActionResult> GetAllStaffList()
        {
            var staffList = await _adminModule.ExecuteQueryAsync(new GetAllStaffsQuery());
            return Ok(staffList);
        }

        [HttpGet("{id}")]
        [HasPermission(AdministrationPermissions.GetAllStaffList)]
        public async Task<IActionResult> StaffById(string id)
        {
            var staff = await _adminModule.ExecuteQueryAsync(new GetStaffByIdQuery(new Guid(id)));
            return Ok(staff);
        }
    }
}