using BuberDiner.Application.Services.Authentication;
using BuberDiner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.WebApi.Controllers;

[Route("api/v1/auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public IActionResult Register(RegisterRequest registerRequest)
    {
        ErrorOr<AuthenticationResult> authResult =
            _authenticationService.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password);

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
    public IActionResult Login(LoginRequest loginRequest)
    {
        ErrorOr<AuthenticationResult> loginResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);

        if (loginResult.IsError && loginResult.FirstError == Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: loginResult.FirstError.Description);
        }

        return loginResult.Match(
            resultValue => Ok(MapAuthResult(resultValue)),
            errors => Problem(errors));
    }
}