using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // [Authorize]
        [HttpGet("listar-todos")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                // Busca todos os usuários usando o serviço de usuário
                var usuarios = await _userService.GetAllUsers();

                // Verifica se a lista de usuários está vazia
                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound("Nenhum usuário encontrado.");
                }

                // Retorna a lista de usuários como resposta JSON
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna um erro genérico
                return StatusCode(500, $"Erro ao buscar usuários: {ex.Message}");
            }
        }

        // [Authorize]
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
                token = UserService.CriarToken(),
                expiraToken = DateTime.Today.AddDays(5)                
            };
            
            await _userService.CreateUser(user);

            return Ok("Usuário criado com sucesso");
        }

        // [Authorize]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var user = await _userService.Authenticate(request.Email, request.Password);
            if (user == null)
                return BadRequest("Email ou senha incorretos.");

            if (user.expiraToken < DateTime.Today){
                user.token = UserService.CriarToken();            
            }

            // var refreshToken = UserService.CriarToken();
            var refreshToken = user.token;

            var userLogged = new UserLoginResponse(user, refreshToken);

            return Ok(userLogged);
        }

        // verifica se token ativo ainda é valido
        // atualiza o token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Verificar(RefreshTokenDTO request)
        {
            var user = await _userService.GetUserByToken(request.Token);
            if (user == null )
                return BadRequest("Token inválido.");

            if (user.email != request.Email)
                return BadRequest("Não há usuários com este email");

            user.token = UserService.CriarToken();
            user.expiraToken = DateTime.Today.AddDays(5);
            await _userService.SaveChanges();

            var refreshedToken = new RefreshTokenDTO(user.email, user.token);

            return Ok(refreshedToken);
        }


        [HttpPost("alterar-senha")]
        public async Task<IActionResult> ResetarSenha(ResetPasswordDTO request)
        {
            var user = await _userService.GetUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Usuário não encontrado.");

            // var userToken = await _userService.GetUserByToken(request.Token);
            if (user.token == null || user.expiraToken < DateTime.Today)
                return BadRequest("Token inválido.");

            UserService.CriarSenhaHash(request.NewPassword, out byte[] senhaHash, out byte[] senha);
            user.senhaHash = senhaHash;
            user.senha = senha;
            // user.reseteSenha = null;
            // user.expiraToken = null;

            await _userService.SaveChanges();

            return Ok("Senha alterada com sucesso.");
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


    }
}