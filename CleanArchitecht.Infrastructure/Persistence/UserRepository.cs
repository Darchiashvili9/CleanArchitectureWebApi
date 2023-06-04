using Azure;
using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.Entities;
using CleanArchitecht.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecht.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;


        public UserRepository(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Add(User user)
        {
            var userExists = await _userManager.FindByNameAsync(user.Email);

            if (userExists != null)
            {
                throw new Exception("User exist!");
            }

            await this._userManager.CreateAsync(user, user.Password);
            await this._userManager.AddToRoleAsync(user, user.Role);
        }

        public async Task<User?> GetUserByEmail(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return user;
            }

            return null;

        }
    }
}
