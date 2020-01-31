using IGRMgr.Frontend.Contracts;
using IGRMgr.Frontend.Utility.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace IGRMgr.Frontend.Utility
{
    public class AccessTokenComponent : IAccessTokenComponent
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _context;

        public AccessTokenComponent(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private string Token { get; set; }
        private DateTime? ExpiryTime { get; set; }

        public async Task<string> GetTokenAsync()
        {
            var now = DateTime.UtcNow;

            
            Token = await _context.HttpContext.GetTokenAsync("access_token");
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(Token);
            var exp = jwtToken.Claims.Where(c => c.Type == "exp").FirstOrDefault().Value;
            var expiryTime = double.Parse(exp).UnixTimeStampToDateTime();
            if (expiryTime > now)
                return Token;
            else
            {
                Token = string.Empty;
                string referer = _context.HttpContext.Request.Headers["Referer"].ToString();

                //log out user first
                await _context.HttpContext.SignOutAsync("Cookies");
                await _context.HttpContext.SignOutAsync("oidc");


                await _context.HttpContext.ChallengeAsync("oidc", new AuthenticationProperties { RedirectUri = referer });

                return Token;
            }
        }
    }
}
