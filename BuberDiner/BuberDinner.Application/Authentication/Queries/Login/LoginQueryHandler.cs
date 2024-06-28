using BuberDinner.Domain.Common.Errors;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;


    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Validate user existence
        if (_userRepository.GetUserByEmail(query.Email) is not { } user) return Errors.Authentication.InvalidCredentials;

        // Validate if password is correct
        if (user.Password != query.Password) return Errors.Authentication.InvalidCredentials;

        // Generate JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        // Return JWT Token
        return new AuthenticationResult(
            user,
            token
        );
    }
}