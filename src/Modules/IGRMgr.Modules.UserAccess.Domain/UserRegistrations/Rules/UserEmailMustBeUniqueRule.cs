using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Domain.UserRegistrations.Rules
{
    public class UserEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly IUsersCounter _usersCounter;
        private readonly string _email;

        public UserEmailMustBeUniqueRule(IUsersCounter usersCounter, string email)
        {
            _usersCounter = usersCounter;
            _email = email;
        }

        public string Message => "User Email already exists.";

        public bool IsBroken() => _usersCounter.ExistingUsersCount(_email) > 0;
    }
}
