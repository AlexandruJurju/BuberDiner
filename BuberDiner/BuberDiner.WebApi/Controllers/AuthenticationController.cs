using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Common;
using BuberDiner.Application.Authentication.Queries.Login;
using BuberDiner.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.WebApi.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
            resultValue => Ok(MapAuthResult(resultValue)),
            errors => Problem(errors));
    }

    private AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );

        return response;
    }

    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var query = new LoginQuery(loginRequest.Email, loginRequest.Password);
        var loginResult = await _mediator.Send(query);

        if (loginResult.IsError && loginResult.FirstError == Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: loginResult.FirstError.Description);

        return loginResult.Match(
            resultValue => Ok(MapAuthResult(resultValue)),
            errors => Problem(errors));
    }
}