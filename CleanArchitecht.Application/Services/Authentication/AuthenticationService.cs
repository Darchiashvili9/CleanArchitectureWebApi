using CleanArchitecht.Application.Common.Interfaces.Authentication;
using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.Common.Errors;
using CleanArchitecht.Domain.Entities;
using ErrorOr;
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

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //ვამოწმებთ იუზერი გვყავს უკვე თუ არა

            if (_userRepository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DublicateEmail;
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

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            //ვამოწმებთ რომ იუზერი არსებობს

            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            //პაროლს ვამოწმებთ

            if (user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            //ვქმნით ტოკენს

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
