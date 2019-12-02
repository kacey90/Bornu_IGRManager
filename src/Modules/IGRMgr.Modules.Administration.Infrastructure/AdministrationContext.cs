using IGRMgr.Modules.Administration.Domain.Staffs;
using IGRMgr.Modules.Administration.Infrastructure.InternalCommands;
using IGRMgr.Modules.Administration.Infrastructure.Staffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nubalance.BuildingBlocks.Infrastructure.InternalCommands;
using Nubalance.BuildingBlocks.Infrastructure.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Infrastructure
{
    public class AdministrationContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public AdministrationContext(DbContextOptions<AdministrationContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        public DbSet<InternalCommand> InternalCommands { get; set; }
        internal DbSet<OutboxMessage> OutboxMessages { get; set; }
        internal DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
        }

    }
}
