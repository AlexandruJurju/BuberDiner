using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Common;
using BuberDiner.Application.Authentication.Queries.Login;
using BuberDiner.Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.WebApi.Controllers;

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
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var registerResult = await _mediator.Send(command);

        return registerResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            Problem);
    }

    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);

        if (loginResult.IsError && loginResult.FirstError == Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: loginResult.FirstError.Description);

        return loginResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            Problem);
    }
}