
using MediatR;
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
            OpenIddictRequest? request = command.Request;
            if (request?.IsClientCredentialsGrantType() is not null)
            {

                var application = await applicationManager.FindByClientIdAsync(request.ClientId);
                BaseException.ThrowIfNull(application, "This clientId was not found");

                ClaimsIdentity identity = new(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                
                //Sample claim setup
                identity.AddClaim(Claims.Subject, "Test Token");
                identity.AddClaim(Claims.Name, "Test Name").SetAudiences("BaseStack");
                identity.AddClaim(Claims.Audience, "BaseStack").SetAudiences("BaseStack");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                
                claimsPrincipal.SetScopes(request.GetScopes());

                response.PrincipalContext = claimsPrincipal;


            }
            return response;
        } 
    }
}
