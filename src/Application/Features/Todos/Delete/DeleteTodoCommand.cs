namespace EGG.CleanAspire.Application.Features.Todos.Delete;

public sealed record DeleteTodoCommand(Guid Id) : ICommand;
