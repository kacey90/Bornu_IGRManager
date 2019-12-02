using Autofac;
using Nubalance.BuildingBlocks.EventBus;
using Nubalance.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Configuration.EventBus
{
    internal class EventBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEventBusClient>()
                .As<IEventBus>()
                .SingleInstance();
        }
    }
}
