using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_api.Domain.DTOs
{
    public class ResetPasswordDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}