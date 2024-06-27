using BuberDiner.Application.Services.Authentication;
using BuberDiner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.WebApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController : ControllerBase
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
        var authResult = _authenticationService.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );

        return Ok(response);
    }

    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthenticationResponse))]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var loginResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);

        var response = new AuthenticationResponse(
            loginResult.Id,
            loginResult.FirstName,
            loginResult.LastName,
            loginResult.Email,
            loginResult.Token
        );

        return Ok(response);
    }
}