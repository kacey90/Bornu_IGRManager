using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.Staffs
{
    public interface IStaffRepository
    {
        Task AddAsync(Staff staff);
        Task<Staff> GetByIdAsync(StaffId staffId);
    }
}
