using Authentication.Application.Commands.Clients.CreateClient;
using MediatR;
using System.Security.Claims;

namespace Authentication.Application.Commands.Token.GetToken
{
    public class GetTokenResponse : IRequest<CreateClientCommandResponse>
    {
        public ClaimsPrincipal? PrincipalContext { get; set; }
    }
}
