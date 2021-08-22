using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IOptionsSnapshot<Configuration> configuration;

        public TokenController(IOptionsSnapshot<Configuration> configuration)
        {
            this.configuration = configuration;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get([FromQuery] string username, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest();
            }

            ///Mock authentication
            if (username != "admin" && password != "1234")
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Value.JWTConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())};

            var token = new JwtSecurityToken(configuration.Value.JWTConfig.Issuer,
                configuration.Value.JWTConfig.Audience,
                claims,
                 expires: DateTime.Now.AddMinutes(configuration.Value.JWTConfig.ExpireMinutes),
                signingCredentials: credentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }

       
    }
}
