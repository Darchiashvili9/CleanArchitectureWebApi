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
        public override string Id { get; set; } = null!;
        public override string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public override string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "user";

    }
}