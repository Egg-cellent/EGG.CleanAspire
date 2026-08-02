using EGG.CleanAspire.Domain.Common;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Todos.Create;

public sealed record CreateTodoCommand(string Title, string? Description) : ICommand<Result<CreateTodoResponse>>;

public sealed record CreateTodoResponse(Guid Id, string Title, string? Description);
