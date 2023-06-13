using CleanArchitecht.Application.Authentication.Common;
using CleanArchitecht.Application.Common.Interfaces.Authentication;
using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitecht.Domain.Common.Errors.Errors;

namespace CleanArchitecht.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {

            //check if user already exists

            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DublicateEmail;
            }

            //create user

            var user = new CleanArchitecht.Domain.Entities.User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password,
            };

            _userRepository.Add(user);

            //create jwt token
            Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user,token);

        }
    }
}
