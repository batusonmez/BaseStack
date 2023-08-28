using MediatR;
using OpenIddict.Abstractions;
using OpenIddict.Core;

namespace Authentication.Application.Commands.Clients.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CreateClientCommandResponse>
    {
        private readonly IOpenIddictApplicationManager openIddictApplicationManager;

        public CreateClientCommandHandler(IOpenIddictApplicationManager openIddictApplicationManager)
        {
            this.openIddictApplicationManager = openIddictApplicationManager;
        }
        public async Task<CreateClientCommandResponse> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            CreateClientCommandResponse response = new();
            try
            {
                var client = await openIddictApplicationManager.FindByClientIdAsync(request.ClientId);
                if (client is null)
                {
                    await openIddictApplicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = request.ClientId,
                        ClientSecret = request.ClientSecret,
                        DisplayName = request.DisplayName,
                        Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.Prefixes.Scope + "read",
                    OpenIddictConstants.Permissions.Prefixes.Scope + "write"
                }
                    });
                    response.Successful = true;

                }

            }
            catch (Exception ex)
            {

                throw;
            }
     
            
            return response;
        }
    }
}
