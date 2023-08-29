using Microsoft.Extensions.Options;
using Northwind.Application.i18n;
using Northwind.Application.Models.Configuration;
using Northwind.Application.Models.Exceptions;
using Northwind.Application.Services.Token;

namespace Northwind.Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfig config;

        public TokenService(IOptions<TokenConfig> config)
        {
            this.config = config.Value;
        }

        public async Task<string?> GetSampleToken()
        {
            using (HttpClient httpClient = new HttpClient())
            { 
                NorthwindException.ThrowIf(!config.IsValid, TokenResource.InvalidTokenConfig);

                var formData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", config.ClientID),
                    new KeyValuePair<string, string>("client_secret", config.ClientSecret),
                    new KeyValuePair<string, string>("grant_type", config.GrantType),
                    new KeyValuePair<string, string>("scope", "read write")
                });
                 
                HttpResponseMessage response = await httpClient.PostAsync(config.TokenURL, formData);
                 
                if (response.IsSuccessStatusCode)
                { 
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                { 
                    throw new NorthwindException(TokenResource.UnableToFetchToken);
                } 
            }

        }
    }
}
