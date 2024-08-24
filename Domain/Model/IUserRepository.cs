using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coin_api.Domain.DTOs;

namespace coin_api.Domain.Model
{
    public interface IUserRepository
    {
        void Add(User user);

        List<UserDTO> Get();
    }
}