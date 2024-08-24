using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coin_api.Domain.DTOs;
using coin_api.Domain.Model;

namespace coin_api.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> Get()
        {
            return _context.Users.ToList();
        }

        List<UserDTO> IUserRepository.Get()
        {
            throw new NotImplementedException();
        }
    }
}