
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;
using System.Security.Claims;
using Authentication.Application.Commands.Clients.CreateClient;
using Microsoft.AspNetCore.Components;
using Dispatcher;
using Authentication.Application.Commands.Token.GetToken;
using OpenIddict.Abstractions;
using Microsoft.AspNetCore;

namespace Authentication.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IDispatcher dispatcher;

        public AuthorizationController(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpPost("~/connect/token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Exchange()
        {
            OpenIddictRequest? request = HttpContext.GetOpenIddictServerRequest();
            if(request == null)
            {
                return BadRequest();
            }
            GetTokenResponse? result = await dispatcher.Send<GetTokenResponse>(new GetTokenCommand()
            {
                Request = request
            }) ;

            if (result?.PrincipalContext == null)
            {
                return NoContent();
            }

            return SignIn(result.PrincipalContext, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
