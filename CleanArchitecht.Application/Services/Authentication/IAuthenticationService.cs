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
        Task<ErrorOr<AuthenticationResult>> Register(string userName, string email, string password);
        Task<ErrorOr<AuthenticationResult>> Login(string email, string password);
    }
}
