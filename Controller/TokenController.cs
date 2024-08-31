using coin_api.Application.Service;
using coin_api.Domain.DTOs;
using coin_api.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace coin_api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // REFATORAR

        private readonly UserService _userService;

        public TokenController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/create-token")]
        public async Task<IActionResult> CreateToken([FromBody] UserLoginDTO Input)
        {
            if (string.IsNullOrWhiteSpace(Input.Email) || string.IsNullOrWhiteSpace(Input.Password))
                return Unauthorized();

            var user = await _userService.Authenticate(Input.Email, Input.Password);

            if (user != null) 
            {
                var token = new TokenJwtBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("JGHF4W3KHUG2867RUYFSDUIYFDT%DBHAJHKSFFY%"))
                    .AddSubject("identityAPI")
                    .AddIssuer("identityAPI.Security.Bearer")
                    .AddAudience("identityAPI.Security.Bearer")
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
                // return Ok(new { token = token.value, email = user.Email });

            }

            return Unauthorized();
        } 
    }
}