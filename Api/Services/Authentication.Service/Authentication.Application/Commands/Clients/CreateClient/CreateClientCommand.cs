using MediatR;

namespace Authentication.Application.Commands.Clients.CreateClient
{
    public class CreateClientCommand : IRequest<CreateClientCommandResponse>
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string DisplayName { get; set; }
    }
}
