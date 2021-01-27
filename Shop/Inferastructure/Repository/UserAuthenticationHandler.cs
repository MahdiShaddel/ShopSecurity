using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shop.Inferastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Shop.Inferastructure.Repository
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {

    }
    public class UserAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private IUserAuthenticationManager userauthenticationManager;
        public UserAuthenticationHandler(
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUserAuthenticationManager userAuthenticationManager) :
        base(options, logger, encoder, clock)
        {
            this.userauthenticationManager = userAuthenticationManager;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                return ValidateToken(token);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
        }
        private AuthenticateResult ValidateToken(string token)
        {
            var validatedToken = userauthenticationManager.Tokens.FirstOrDefault(t => t.Key == token);
            if (validatedToken.Key == null)
            {
                return AuthenticateResult.Fail("Unauthorize");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, validatedToken.Value.Item1),
                new Claim(ClaimTypes.Role, validatedToken.Value.Item2)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new GenericPrincipal(identity, new[] { validatedToken.Value.Item2 });
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
