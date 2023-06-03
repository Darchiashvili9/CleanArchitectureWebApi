using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(s => s.Email == email);
        }
    }
}
