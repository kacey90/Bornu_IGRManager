using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Infrastructure.Outbox
{
    public interface IOutbox
    {
        void Add(OutboxMessage message);
    }
}
