using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Staffs
{
    public class StaffCreatedNotificationHandler : INotificationHandler<StaffCreatedNotification>
    {
        public Task Handle(StaffCreatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
