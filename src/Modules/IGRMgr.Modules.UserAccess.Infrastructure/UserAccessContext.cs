using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using IGRMgr.Modules.UserAccess.Infrastructure.InternalCommands;
using IGRMgr.Modules.UserAccess.Infrastructure.Outbox;
using IGRMgr.Modules.UserAccess.Infrastructure.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nubalance.BuildingBlocks.Infrastructure.InternalCommands;
using Nubalance.BuildingBlocks.Infrastructure.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Infrastructure
{
    public class UserAccessContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public UserAccessContext(DbContextOptions<UserAccessContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        //public UserAccessContext()
        //{

        //}

        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRegistrationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
        }
    }
}
