using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_api.Application.Service
{
    public class UserService
    {
        private readonly ConnectionContext _context;

        public UserService(ConnectionContext context)
        {
            _context = context;
        }
    }
}