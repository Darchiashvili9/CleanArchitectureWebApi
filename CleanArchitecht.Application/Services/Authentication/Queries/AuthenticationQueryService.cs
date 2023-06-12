using CleanArchitecht.Application.Common.Interfaces.Authentication;
using CleanArchitecht.Application.Services.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService:IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(), "firstanem", "lastName", email, "token");
        }

    }
}
