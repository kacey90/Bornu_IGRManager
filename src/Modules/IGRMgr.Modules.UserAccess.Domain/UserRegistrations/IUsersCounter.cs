using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Domain.UserRegistrations
{
    public interface IUsersCounter
    {
        int ExistingUsersCount(string email);
    }
}
