using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DublicateEmail => Error.Conflict(code: "user.DuplicateEmail", description: "Email already in use");
        }
    }
}
