using EGG.CleanAspire.Domain.Common;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Identity.RefreshToken;

public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<Result<TokenResponse>>;
