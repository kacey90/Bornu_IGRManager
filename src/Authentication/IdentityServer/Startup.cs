// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer.Configuration;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace IdentityServer
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                //.AddUserSecrets<Startup>()
                .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Authentication.CookieLifetime = TimeSpan.FromSeconds(1800);
            })
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                })
                .AddAspNetIdentity<ApplicationUser>();

            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "<insert here>";
                    options.ClientSecret = "<insert here>";
                })
                .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "native.code";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });

            services.AddTransient<IInitializeConfiguration, InitializeConfiguration>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

                serviceScope.ServiceProvider.GetRequiredService<IInitializeConfiguration>().InitializeFiles();

                var roleService = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var superAdmin = roleService.FindByNameAsync("Super Administrator").Result;
                if (superAdmin == null)
                {
                    superAdmin = new IdentityRole
                    {
                        Name = "Super Administrator"
                    };
                    var result = roleService.CreateAsync(superAdmin).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                var admin = roleService.FindByNameAsync("Administrator").Result;
                if (admin == null)
                {
                    admin = new IdentityRole
                    {
                        Name = "Administrator"
                    };
                    var result = roleService.CreateAsync(admin).Result;
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);
                }

                var manager = roleService.FindByNameAsync("Manager").Result;
                if (manager == null)
                {
                    manager = new IdentityRole
                    {
                        Name = "Manager"
                    };
                    var result = roleService.CreateAsync(manager).Result;
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);
                }

                var staff = roleService.FindByNameAsync("Staff").Result;
                if (staff == null)
                {
                    staff = new IdentityRole
                    {
                        Name = "Staff"
                    };
                    var result = roleService.CreateAsync(staff).Result;
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);
                }

                var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var sa = userMgr.FindByNameAsync("admin").Result;
                if (sa == null)
                {
                    sa = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@email.com",
                        EmailConfirmed = true
                    };
                    var result = userMgr.CreateAsync(sa, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddToRoleAsync(sa, "Administrator").Result;
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    var role = string.Empty;
                    var aliceRoles = userMgr.GetRolesAsync(sa).Result;

                    result = userMgr.AddClaimsAsync(sa, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Admin Adminstrator"),
                        new Claim(JwtClaimTypes.GivenName, "Admin"),
                        new Claim(JwtClaimTypes.FamilyName, "Administrator"),
                        new Claim(JwtClaimTypes.Role, string.Join(",", aliceRoles)),
                        new Claim(JwtClaimTypes.Email, "admin@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://breezeonline.ng"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("admin created");
                }
                else
                {
                    Console.WriteLine("admin already exists");
                }
            }
        }
    }
}