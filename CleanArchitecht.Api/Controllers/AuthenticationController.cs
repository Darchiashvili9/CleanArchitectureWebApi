using CleanArchitecht.Application.Authentication.Commands.Register;
using CleanArchitecht.Application.Authentication.Common;
using CleanArchitecht.Application.Authentication.Queries.Login;
using CleanArchitecht.Contracts.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecht.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
                );

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);


            return authResult.Match(
                 authResult => Ok(MapUthResult(authResult)),
                 errors => Problem(errors));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(
                request.Email,
                request.Password
                );


            var authResult = await _mediator.Send(query);

            return authResult.Match(
                 authResult => Ok(MapUthResult(authResult)),
                 errors => Problem(errors));
        }

        private static AuthenticationResponse MapUthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                  authResult.User.Id,
                  authResult.User.FirstName,
                  authResult.User.LastName,
                  authResult.User.Email,
                  authResult.Token);
        }

    }
}
