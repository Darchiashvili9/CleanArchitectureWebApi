using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecht.Application.Services.Authentication.Common;

namespace CleanArchitecht.Application.Services.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        AuthenticationResult Login(string email, string password);

    }
}
