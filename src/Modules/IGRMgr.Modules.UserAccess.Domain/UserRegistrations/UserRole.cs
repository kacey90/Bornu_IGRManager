using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Domain.UserRegistrations
{
    public class UserRole : ValueObject
    {
        public static UserRole Staff => new UserRole(nameof(Staff));
        public static UserRole Manager => new UserRole(nameof(Manager));
        public static UserRole Administrator => new UserRole(nameof(Administrator));
        public static UserRole BusinessPartner => new UserRole(nameof(BusinessPartner));
        public string Value { get; }

        public UserRole(string value)
        {
            this.Value = value;
        }
    }
}
