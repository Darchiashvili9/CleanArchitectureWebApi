using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Contracts.Authentication
{
    public record AuthenticationResponse(
        string Id,
        string Email,
        string Token);
}
