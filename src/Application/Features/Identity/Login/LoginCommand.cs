using EGG.CleanAspire.Domain.Common;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Identity.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<Result<TokenResponse>>;
