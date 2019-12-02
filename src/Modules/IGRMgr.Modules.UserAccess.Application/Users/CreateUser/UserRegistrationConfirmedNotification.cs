using IGRMgr.Modules.UserAccess.Domain.UserRegistrations.Events;
using Newtonsoft.Json;
using Nubalance.BuildingBlocks.Infrastructure.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.CreateUser
{
    public class UserRegistrationConfirmedNotification : DomainNotificationBase<UserRegistrationConfirmedDomainEvent>
    {
        [JsonConstructor]
        public UserRegistrationConfirmedNotification(UserRegistrationConfirmedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
