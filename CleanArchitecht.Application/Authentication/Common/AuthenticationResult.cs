using CleanArchitecht.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Authentication.Common
{
    public record AuthenticationResult(User User, string Token);
}
