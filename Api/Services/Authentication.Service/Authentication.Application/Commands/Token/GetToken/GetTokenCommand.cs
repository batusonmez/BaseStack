using MediatR;
using OpenIddict.Abstractions;

namespace Authentication.Application.Commands.Token.GetToken
{
    public class GetTokenCommand : IRequest<GetTokenResponse>
    {
        public OpenIddictRequest? Request { get; set; }
    }
}
