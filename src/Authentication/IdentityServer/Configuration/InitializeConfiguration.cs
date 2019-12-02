using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class InitializeConfiguration : IInitializeConfiguration
    {
        private readonly ConfigurationDbContext _context;
        private IWebHostEnvironment _env;

        public InitializeConfiguration(ConfigurationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public void InitializeFiles()
        {
            string contentRootPath = _env.ContentRootPath;
            var clientJson = File.ReadAllText(contentRootPath + "/Configuration/clients.json");
            var apiResourceJson = File.ReadAllText(contentRootPath + "/Configuration/apiresources.json");

            var clients = JsonConvert.DeserializeObject<IEnumerable<Client>>(clientJson);
            var apiResources = JsonConvert.DeserializeObject<IEnumerable<ApiResource>>(apiResourceJson);

            bool lAdd = false;
            IList<Client> clientsToAdd = new List<Client>();
            foreach(var client in clientsToAdd)
            {
                var dbClient = _context.Clients
                    .Where(c => c.ClientId == client.ClientId)
                    .Include(p => p.Properties)
                    .FirstOrDefault();

                if (dbClient == null)
                {
                    lAdd = true;
                    clientsToAdd.Add(client);
                }
                else
                {
                    if (dbClient.Properties.Count > 0)
                    {
                        int.TryParse(dbClient.Properties.FirstOrDefault(c => c.Key == "version").Value, out int dbVersion);
                        int.TryParse(client.Properties.FirstOrDefault(c => c.Key == "version").Value, out int version);
                        if (dbVersion < version)
                        {
                            _context.Clients.Remove(dbClient);
                            _context.SaveChanges();
                            lAdd = true;
                            clientsToAdd.Add(client);
                        }
                    }
                    else
                    {
                        _context.Clients.Remove(dbClient);
                        _context.SaveChanges();
                        lAdd = true;
                        clientsToAdd.Add(client);
                    }
                }
            }

            foreach (var item in clientsToAdd)
            {
                IList<Secret> secrets = new List<Secret>
                {
                    new Secret("igrAppMvc-secret".Sha256())
                };
                item.ClientSecrets = secrets;
                item.AllowedGrantTypes = GrantTypes.Code;
                item.RefreshTokenExpiration = TokenExpiration.Absolute;
                _context.Clients.Add(item.ToEntity());
            }
            _context.SaveChanges();

            if (!_context.IdentityResources.Any())
            {
                foreach (var resource in Config.Ids)
                {
                    _context.IdentityResources.Add(resource.ToEntity());
                }
                _context.SaveChanges();
            }

            IList<ApiResource> resourcesToAdd = new List<ApiResource>();
            foreach (var resource in apiResources)
            {
                var dbReource = _context.ApiResources
                    .FirstOrDefault(c => c.Name == resource.Name);

                if (dbReource != null)
                {
                    if (lAdd)
                    {
                        _context.ApiResources.Remove(dbReource);
                        _context.SaveChanges();
                        resourcesToAdd.Add(resource);
                    }
                }
                else
                {
                    resourcesToAdd.Add(resource);
                }
            }

            foreach (var item in resourcesToAdd)
            {
                _context.ApiResources.Add(item.ToEntity());
            }
            _context.SaveChanges();
        }
    }

    public interface IInitializeConfiguration
    {
        void InitializeFiles();
    }
}
