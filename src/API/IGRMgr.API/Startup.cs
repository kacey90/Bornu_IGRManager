using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using IGRMgr.API.Configuration.Authorization;
using IGRMgr.API.Configuration.Validation;
using IGRMgr.Modules.UserAccess.Application.Configuration;
using IGRMgr.Modules.UserAccess.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nubalance.BuildingBlocks.Application;
using Nubalance.BuildingBlocks.Domain;
using Nubalance.BuildingBlocks.Infrastructure;
using Serilog;
using Serilog.Formatting.Compact;
//using Microsoft.Extensions.Logging;

namespace IGRMgr.API
{
    public class Startup
    {
        private const string AppConnectionString = "IGR_ConnectionString";
        private static ILogger _logger;
        private static ILogger _loggerForApi;
        public Startup(IWebHostEnvironment environment)
        {
            ConfigureLogger();

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddUserSecrets<Startup>()
                .Build();
        }

        private IConfiguration Configuration { get; }
        //internal ContainerBuilder ContainerBuilder { get; private set; }
        public ILifetimeScope AutofacContainer { get; private set; }

        //public IServiceProvider Services { get; private set; }
        public IServiceCollection Services { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddHttpClient();
            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
                    policyBuilder.AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme);
                });
            });

            bool.TryParse(Configuration["AuthOptions:RequireHttps"], out bool requireHttps);
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Authority = Configuration["AuthOptions:Authority"];
                    x.ApiName = Configuration["AuthOptions:Audience"];
                    x.RequireHttpsMetadata = requireHttps;
                    x.SaveToken = true;
                });

            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();

            services.AddSwaggerDocumentation();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //var container = builder.Build();

            //var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            //var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            ServicesAutofacExtensions.RegisterServicesOnAutofac(builder, migrationsAssembly, Configuration, _loggerForApi);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseMiddleware<CorrelationMiddleware>();
            app.UseSwaggerDocumentation();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }
    }

    public class UserAccessContextFactory : IDesignTimeDbContextFactory<UserAccessContext>
    {
        public UserAccessContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UserAccessContext>();
            //optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), sqlServerOptions =>
            //            sqlServerOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

            return new UserAccessContext(optionsBuilder.Options, null);
        }
    }
}
