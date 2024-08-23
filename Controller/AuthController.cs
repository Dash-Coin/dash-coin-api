
using coin_api.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace coin_api.Controller
{
    [ApiController]
    [Route("coin-api/v1/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "teste" && password == "123")
            {
                var token = TokenService.GenerateToken(new Domain.Model.User());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }

    }
}