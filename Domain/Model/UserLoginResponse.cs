using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_api.Domain.Model
{
    public class UserLoginResponse
    {
        public User User { get; set; }
        public string RefreshToken { get; set; }

        public UserLoginResponse(User user, string refreshToken)
        {
            User = user;
            RefreshToken = refreshToken;
        }
    }
}