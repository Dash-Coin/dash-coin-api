using coin_api.Entities;
using coin_api.Models;
using coin_api.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace coin_api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // REFATORAR

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public TokenController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] InputLoginRequest Input)
        {
            if (string.IsNullOrWhiteSpace(Input.Email) || string.IsNullOrWhiteSpace(Input.Password))
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);

            if (result.Succeeded) 
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("JGHF4W3KHUG2867RUYFSDUIYFDT%DBHAJHKSFFY%"))
                    .AddSubject("identityAPI")
                    .AddIssuer("identityAPI.Security.Bearer")
                    .AddAudience("identityAPI.Security.Bearer")
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);

            }

            return Unauthorized();
        } 
    }
}