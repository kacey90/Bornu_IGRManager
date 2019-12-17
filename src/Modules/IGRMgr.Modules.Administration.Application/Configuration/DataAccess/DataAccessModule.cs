using Autofac;
using IGRMgr.Modules.Administration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Nubalance.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Configuration.DataAccess
{
    internal class DataAccessModule : Module
    {
        private readonly string _databaseConnectionString;
        private readonly ILoggerFactory _loggerFactory;
        private readonly string _migrationsAssembly;

        internal DataAccessModule(string databaseConnectionString, string migrationsAssemly, ILoggerFactory loggerFactory)
        {
            _databaseConnectionString = databaseConnectionString;
            _loggerFactory = loggerFactory;
            _migrationsAssembly = migrationsAssemly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<AdministrationContext>();
                    dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString, sqlServerOptions => 
                        sqlServerOptions.MigrationsAssembly(_migrationsAssembly));

                    dbContextOptionsBuilder
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                    return new AdministrationContext(dbContextOptionsBuilder.Options, _loggerFactory);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            var infrastructureAssembly = typeof(AdministrationContext).Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder()); ;
        }
    }
}
