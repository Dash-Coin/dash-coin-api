using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using coin_api.Domain.Model;
using coin_api.Token;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace coin_api.Application.Service
{
    public class UserService
    {
        private readonly ConnectionContext _context;

        public UserService(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.email == email);
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == email);
            if (user == null || !VerificarHashSenha(password, user.senhaHash, user.senha))
            {
                return null;
            }

            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<User?> GetUserByToken(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.verificaToken == token);
        }

        public async Task CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public static void CriarSenhaHash(string SenhaForte, out byte[] senhaHash, out byte[] senha)
        {
            using var hmac = new HMACSHA512();
            senha = hmac.Key;
            senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(SenhaForte));
        }

        public static bool VerificarHashSenha(string senhaForte, byte[] senhaHash, byte[] senha)
        {
            using (var hmac = new HMACSHA512(senha))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaForte));
                return computedHash.SequenceEqual(senhaHash);
            }
        }

        public static string CriarTokenAleatorio()
        {
            var token = new TokenJwtBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("JGHF4W3KHUG2867RUYFSDUIYFDT%DBHAJHKSFFY%"))
                    .AddSubject("identityAPI")
                    .AddIssuer("identityAPI.Security.Bearer")
                    .AddAudience("identityAPI.Security.Bearer")
                    .AddExpiry(5)
                    .Builder();

            return token.value;
            // return Convert.ToHexString(RandomNumberGenerator.GetBytes(8));
        }
    }
}