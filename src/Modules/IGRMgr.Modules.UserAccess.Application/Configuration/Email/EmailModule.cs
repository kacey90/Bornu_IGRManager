using Autofac;
using Nubalance.BuildingBlocks.Infrastructure.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Configuration.Email
{
    internal class EmailModule : Module
    {
        private readonly EmailsConfiguration _configuration;

        public EmailModule(EmailsConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailSender>()
                .As<IEmailSender>()
                .WithParameter("configuration", _configuration)
                .InstancePerLifetimeScope();
        }
    }
}
