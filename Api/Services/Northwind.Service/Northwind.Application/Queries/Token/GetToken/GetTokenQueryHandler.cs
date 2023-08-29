using MediatR;
using Northwind.Application.Services.Token;

namespace Northwind.Application.Queries.Token.GetToken
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, GetTokenQueryResponse>
    {
        private readonly ITokenService tokenService;

        public GetTokenQueryHandler(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        public async Task<GetTokenQueryResponse> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {            
            GetTokenQueryResponse response = new();
            response.Token= await tokenService.GetSampleToken();
            return   response; 
        }
    }
}
