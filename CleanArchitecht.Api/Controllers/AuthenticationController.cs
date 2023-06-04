using CleanArchitecht.Application.Services.Authentication;
using CleanArchitecht.Contracts.Authentication;
using CleanArchitecht.Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecht.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthenticationController(IAuthenticationService authenticationService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var authResult = await _authenticationService.Register(
                request.UserName,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
                );
            return authResult.Match(authResult => Ok(MapUthResult(authResult)),
                errors => Problem(errors));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResult = await _authenticationService.Login(
                request.Email,
                request.Password
                );

            return authResult.Match(authResult => Ok(MapUthResult(authResult)),
                errors => Problem(errors));
        }



        private static AuthenticationResponse MapUthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                  authResult.user.Id,
                  authResult.user.FirstName,
                  authResult.user.LastName,
                  authResult.user.Email,
                  authResult.Token);
        }
    }
}
