﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Contracts.Authentication
{
    public record RegisterRequest(
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        string Password);
}
