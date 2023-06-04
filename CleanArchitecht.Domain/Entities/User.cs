using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "user";

    }
}