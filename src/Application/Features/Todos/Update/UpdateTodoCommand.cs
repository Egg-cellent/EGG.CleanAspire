using EGG.CleanAspire.Domain.Common;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Todos.Update;

public sealed record UpdateTodoCommand(Guid Id, string Title, string? Description) : ICommand<Result>;
