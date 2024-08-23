using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using coin_api.Application.ViewModel;
using coin_api.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace coin_api.Controller
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(UserViewModel userView)
        {
            var user = new User(userView.Name, userView.Email);
            _userRepository.Add(user);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userRepository.Get();

            return Ok(users);
        }
    }
}