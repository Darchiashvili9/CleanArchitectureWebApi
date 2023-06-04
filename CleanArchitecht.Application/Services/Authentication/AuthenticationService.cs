using CleanArchitecht.Application.Common.Interfaces.Authentication;
using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.Common.Errors;
using CleanArchitecht.Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecht.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ErrorOr<AuthenticationResult>> Register(string userName, string firstName, string lastName, string email, string password)
        {
            //ვამოწმებთ იუზერი გვყავს უკვე თუ არა

            //if (_userRepository.GetUserByEmail(email) is not null)
            //{
            //    return Errors.User.DublicateEmail;
            //}


            var userExists = await _userManager.FindByNameAsync(firstName);

            if (userExists != null)
            {
                // return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }


            //ვქმნით

            var user = new User
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                SecurityStamp = Guid.NewGuid().ToString(),
            };


            ////create jwt token
            //   var token = _jwtTokenGenerator.GenerateToken(user);






            var result = await _userManager.CreateAsync(user, password);

            //    var newUser = _userManager.FindByNameAsync(firstName);




            var roleExist = await _roleManager.RoleExistsAsync("user");
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole("user"));
            }




            await this._userManager.AddToRoleAsync(user, "user");


            if (!result.Succeeded)
            {
                throw new Exception();
            }

            return new AuthenticationResult(user, user.SecurityStamp);

        }

        public async Task<ErrorOr<AuthenticationResult>> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            //ვამოწმებთ რომ იუზერი არსებობს

            if (user is null)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            //პაროლს ვამოწმებთ

            if (user.Password is not null && user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            //ვქმნით ტოკენს

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
