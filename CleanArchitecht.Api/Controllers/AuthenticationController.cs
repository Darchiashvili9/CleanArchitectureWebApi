using CleanArchitecht.Application.Services.Authentication.Commands;
using CleanArchitecht.Application.Services.Authentication.Queries;
using CleanArchitecht.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecht.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationCommandService _authenticationCommandService;
        private IAuthenticationQueryService _authenticationQueryService;

        public AuthenticationController(IAuthenticationCommandService authenticationService, IAuthenticationQueryService authenticationQueryService)
        {
            _authenticationCommandService = authenticationService;
            _authenticationQueryService = authenticationQueryService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationCommandService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
                );

            var response = new AuthenticationResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token);

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationQueryService.Login(
                request.Email,
                request.Password
                );

            var response = new AuthenticationResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token);

            return Ok(response);
        }

    }
}
