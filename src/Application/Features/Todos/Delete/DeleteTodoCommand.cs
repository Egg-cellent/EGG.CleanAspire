using EGG.CleanAspire.Domain.Common;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Todos.Delete;

public sealed record DeleteTodoCommand(Guid Id) : ICommand<Result>;
