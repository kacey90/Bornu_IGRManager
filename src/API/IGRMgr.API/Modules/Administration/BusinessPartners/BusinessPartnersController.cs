using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGRMgr.API.Configuration.Authorization;
using IGRMgr.Modules.Administration.Application.BusinessPartners;
using IGRMgr.Modules.Administration.Application.BusinessPartners.Queries;
using IGRMgr.Modules.Administration.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IGRMgr.API.Modules.Administration.BusinessPartners
{
    [Route("api/administration/[controller]")]
    [ApiController]
    public class BusinessPartnersController : ControllerBase
    {
        private readonly IAdministrationModule _administrationModule;

        public BusinessPartnersController(IAdministrationModule administrationModule)
        {
            _administrationModule = administrationModule;
        }

        [HasPermission(AdministrationPermissions.GetAllBusinessPartners)]
        public async Task<IActionResult> GetAllBusinessPartners()
        {
            var businessPartners = await _administrationModule.ExecuteQueryAsync(new GetAllBusinessPartnersQuery());
            return Ok(businessPartners);
        }

        [HttpGet("{id}")]
        [HasPermission(AdministrationPermissions.GetAllBusinessPartners)]
        public async Task<IActionResult> GetBusinessPartnerById(string id)
        {
            var businessPartner = await _administrationModule.ExecuteQueryAsync(new GetBusinessPartnerByIdQuery(new Guid(id)));
            return Ok(businessPartner);
        }

        [HttpPost]
        [HasPermission(AdministrationPermissions.CreateBusinessPartner)]
        public async Task<IActionResult> CreateBusinessPartner([FromBody] AddNewBusinessPartnerRequest request)
        {
            await _administrationModule.ExecuteCommandAsync(new CreateBusinessPartnerCommand(request.FirstName, request.LastName,
                request.PhoneNumber, request.Email));

            return Ok("Business Partner created");
        }
    }
}