using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_api.Domain.DTOs
{
    public class RefreshTokenDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        
        public RefreshTokenDTO(string email, string token)
        {
            Email = email;
            Token = token;
        }        
    }
}