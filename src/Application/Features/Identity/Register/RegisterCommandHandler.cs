using EGG.CleanAspire.Domain.Common;
using EGG.CleanAspire.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Identity.Register;

public sealed class RegisterCommandHandler(UserManager<ApplicationUser> userManager) : ICommandHandler<RegisterCommand, Result>
{
    public async ValueTask<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.FindByEmailAsync(command.Email);
        if (existingUser is not null)
            return Result.Failure(Error.Conflict("Auth.EmailTaken", "A user with this email already exists."));

        var user = new ApplicationUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            UserName = command.Email
        };

        var result = await userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(Error.Validation("Auth.RegistrationFailed", errors));
        }

        return Result.Success();
    }
}
