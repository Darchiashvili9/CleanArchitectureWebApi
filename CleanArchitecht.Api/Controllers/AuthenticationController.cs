using CleanArchitecht.Application.Authentication.Commands.Register;
using CleanArchitecht.Application.Authentication.Common;
using CleanArchitecht.Application.Authentication.Queries.Login;
using CleanArchitecht.Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecht.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);


            return authResult.Match(
                 authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)),
                 errors => Problem(errors));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginRequest>(request);


            var authResult = await _mediator.Send(query);

            return authResult.Match(
                 authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)),
                 errors => Problem(errors));
        }
    }
}
