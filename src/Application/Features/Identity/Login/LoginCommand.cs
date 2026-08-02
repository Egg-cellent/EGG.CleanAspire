using EGG.CleanAspire.Domain.Common;

namespace EGG.CleanAspire.Application.Features.Identity.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<Result<TokenResponse>>;
