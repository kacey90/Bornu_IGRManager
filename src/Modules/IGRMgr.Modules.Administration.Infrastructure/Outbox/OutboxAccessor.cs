using Nubalance.BuildingBlocks.Infrastructure.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Infrastructure.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly AdministrationContext _context;

        public OutboxAccessor(AdministrationContext context)
        {
            _context = context;
        }

        public void Add(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
        }
    }
}
