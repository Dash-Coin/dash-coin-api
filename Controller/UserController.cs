using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using coin_api.Application.Service;
using coin_api.Application.ViewModel;
using coin_api.Domain.DTOs;
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
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(UserRegisterDTO request)
        {
            if (await _userService.UserExists(request.Email))
                return BadRequest("Usuário já existe");

            UserService.CriarSenhaHash(request.Password, out byte[] senhaHash, out byte[] senha);

            var user = new User
            {
                email = request.Email,
                senhaHash = senhaHash,
                senha = senha,
                token = UserService.CriarToken()
            };

            await _userService.CreateUser(user);

            return Ok("Usuário criado com sucesso");
        }

        [Authorize]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var user = await _userService.Authenticate(request.Email, request.Password);
            if (user == null)
                return BadRequest("Email ou senha incorretos.");

            var refreshToken = UserService.CriarToken();

            var userLogged = new UserLoginResponse(user, refreshToken);

            return Ok(userLogged);
        }

        // verifica se token ativo ainda é valido
        [HttpPost("verificar")]
        public async Task<IActionResult> Verificar(string token)
        {
            var user = await _userService.GetUserByToken(token);
            if (user == null)
                return BadRequest("Token inválido.");

            user.expiraToken = DateTime.Now;
            await _userService.SaveChanges();

            return Ok("Usuário verificado.");
        }

        // [HttpPost("alterar-senha")]
        // public async Task<IActionResult> EsquecerSenha(UserLoginDTO request)
        // {
        //     var user = await _userService.GetUserByEmail(request.Email);
        //     if (user == null)
        //         return BadRequest("Usuário não encontrado.");

        //     // user.reseteSenha = UserService.CriarToken();
        //     user.expiraToken = DateTime.Now.AddDays(5);

        //     await _userService.SaveChanges();

        //     return Ok("Senha alterada");
        // }

        [HttpPost("alterar-senha")]
        public async Task<IActionResult> ResetarSenha(ResetPasswordDTO request)
        {
            var user = await _userService.GetUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Usuário não encontrado.");

            // var userToken = await _userService.GetUserByToken(request.Token);
            if (user.token == null || user.expiraToken < DateTime.Now)
                return BadRequest("Token inválido.");

            UserService.CriarSenhaHash(request.NewPassword, out byte[] senhaHash, out byte[] senha);
            user.senhaHash = senhaHash;
            user.senha = senha;
            // user.reseteSenha = null;
            // user.expiraToken = null;

            await _userService.SaveChanges();

            return Ok("Senha alterada com sucesso.");
        }
    }
}