using CleanArchitecht.Application.Common.Interfaces.Authentication;
using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //ვამოწმებთ იუზერი გვყავს უკვე თუ არა

            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("user with this email already is registered");
            }

            //ვქმნით

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);

            //create jwt token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public AuthenticationResult Login(string email, string password)
        {
            //ვამოწმებთ რომ იუზერი არსებობს

            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("user with given email does not exist");
            }

            //პაროლს ვამოწმებთ

            if (user.Password != password)
            {
                throw new Exception("invalid password");
            }

            //ვქმნით ტოკენს

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
