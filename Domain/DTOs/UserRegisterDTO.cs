using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_api.Domain.DTOs
{
    public class UserRegisterDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        // public string SenhaForte { get; set; } // = string.Empty;
    }
}