using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nubalance.BuildingBlocks.Infrastructure.InternalCommands
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
