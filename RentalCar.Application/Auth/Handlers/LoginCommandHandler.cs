using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Common.Security;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Auth.Handlers;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IValidator<LoginCommand> validator,
    IJwtTokenService jwtTokenService
    ) : ICommandHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(LoginCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        var user = await userRepository.FindByEmailAsync(command.Email);

        if (user == null)
        {
            return Result<string>.Fail("Invalid email or password.", ErrorType.Unauthorized);
        }

        var verify = PasswordHasher.Verify(command.Password, user.PasswordHash);

        if (!verify)
        {
            return Result<string>.Fail("Invalid email or password.", ErrorType.Unauthorized);
        }
        
        var token = jwtTokenService.GenerateToken(user);
        
        return Result<string>.Ok(token, "Login successful.");
    }
}