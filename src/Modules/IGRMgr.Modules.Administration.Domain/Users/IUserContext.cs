using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.Users
{
    public interface IUserContext
    {
        UserId UserId { get; }
    }
}
