
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;
using System.Security.Claims;
using OpenIddict.Abstractions;
using Exceptions;

namespace Authentication.Application.Commands.Token.GetToken
{
    public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, GetTokenResponse>
    {
        private readonly IOpenIddictApplicationManager applicationManager;

        public GetTokenCommandHandler(IOpenIddictApplicationManager applicationManager)
        {
            this.applicationManager = applicationManager;
        }

        public async Task<GetTokenResponse> Handle(GetTokenCommand command, CancellationToken cancellationToken)
        {
            GetTokenResponse response = new();
            var request = command.Request;
            if (request?.IsClientCredentialsGrantType() is not null)
            {
                var application = await applicationManager.FindByClientIdAsync(request.ClientId);
                BaseException.ThrowIfNull(application, "This clientId was not found");

                ClaimsIdentity identity = new(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);


                identity.AddClaim(Claims.Subject, "Test Token");
                identity.AddClaim(Claims.Name, "Test Name");
                identity.AddClaim(JwtRegisteredClaimNames.Aud, "Example-OpenIddict");

                var claimsPrincipal = new ClaimsPrincipal(identity);
                claimsPrincipal.SetScopes(request.GetScopes());

                response.PrincipalContext = claimsPrincipal;


            }
            return response;
        } 
    }
}
