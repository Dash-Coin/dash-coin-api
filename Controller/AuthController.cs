using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using coin_api.Application.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace coin_api.Controller
{
    [ApiController]
    [Route("coin-api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "teste" && password == "123")
            {
                var token = TokenService.GenerateToken(new User());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }

    }
}