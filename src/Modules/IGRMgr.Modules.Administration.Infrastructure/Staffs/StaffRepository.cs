using IGRMgr.Modules.Administration.Domain.Staffs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Infrastructure.Staffs
{
    internal class StaffRepository : IStaffRepository
    {
        private readonly AdministrationContext _administrationContext;

        internal StaffRepository(AdministrationContext administrationContext)
        {
            _administrationContext = administrationContext;
        }
        public async Task AddAsync(Staff staff)
        {
            await _administrationContext.Staffs.AddAsync(staff);
        }

        public async Task<Staff> GetByIdAsync(StaffId staffId)
        {
            return await _administrationContext.Staffs.FirstOrDefaultAsync(x => x.Id == staffId);
        }
    }
}
