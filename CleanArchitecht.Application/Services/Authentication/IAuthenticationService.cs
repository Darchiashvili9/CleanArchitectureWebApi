using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}
