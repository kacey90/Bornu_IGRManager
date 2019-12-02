using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Infrastructure.Emails
{
    public class EmailsConfiguration
    {
        public EmailsConfiguration(string fromEmail)
        {
            FromEmail = fromEmail;
        }

        public string FromEmail { get; }
    }
}
