using IGRMgr.Modules.UserAccess.Domain.Users;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityServerHttpClientFactory _idSvrClient;
        private readonly ILogger _logger;

        public UserRepository(IdentityServerHttpClientFactory idSvrClient, ILogger logger)
        {
            _idSvrClient = idSvrClient;
            _logger = logger.ForContext<UserRepository>();
        }

        public async Task AddAsync(User user)
        {
            var userModel = new UserInput
            {
                Id = user.Id.Value.ToString(),
                FirstName = user._firstName,
                LastName = user._lastName,
                MiddleName = user._middleName,
                Email = user._email,
                Role = user._role,
                Password = user._password
            };

            var clientId = "";
            var secret = "";

            var credentials = string.Format("{0}:{1}", clientId, secret);
            var headerValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            _idSvrClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
            string jsonObj = JsonConvert.SerializeObject(userModel);

            var response = await _idSvrClient.HttpClient.PostAsync("api/AccountAPI",
               new StringContent(jsonObj, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                _logger.Information("User has been created.");
            }
            else
                _logger.Error("User creation failed.");
        }

        private class UserInput
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public string Role { get; set; }
            public string TenantId { get; set; }
        }
    }
}
