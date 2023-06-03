using CleanArchitecht.Application.Services.Authentication;
using CleanArchitecht.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecht.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
                );
            return authResult.Match(authResult => Ok(MapUthResult(authResult)),
                errors => Problem(errors));

        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(
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
