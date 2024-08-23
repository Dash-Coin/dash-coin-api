using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_api.Domain.Model
{
    public interface IUserRepository
    {
        void Add(User user);

        List<User> Get();
    }
}