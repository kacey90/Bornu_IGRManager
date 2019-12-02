using IGRMgr.Modules.UserAccess.Domain.Users.Events;
using Newtonsoft.Json;
using Nubalance.BuildingBlocks.Infrastructure.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.CreateUser
{
    public class UserCreatedNotification : DomainNotificationBase<UserCreatedDomainEvent>
    {
        [JsonConstructor]
        public UserCreatedNotification(UserCreatedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
