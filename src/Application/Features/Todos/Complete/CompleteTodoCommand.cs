namespace EGG.CleanAspire.Application.Features.Todos.Complete;

public sealed record CompleteTodoCommand(Guid Id) : ICommand;
