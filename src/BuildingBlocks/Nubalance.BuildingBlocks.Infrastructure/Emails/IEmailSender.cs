using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Infrastructure.Emails
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessage message);
    }
}
