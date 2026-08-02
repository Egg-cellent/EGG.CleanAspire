using EGG.CleanAspire.Domain.Common;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Todos.Delete;

public sealed class DeleteTodoCommandHandler(IAppDbContext dbContext) : ICommandHandler<DeleteTodoCommand, Result>
{
    public async ValueTask<Result> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await dbContext.Todos.FindAsync([command.Id], cancellationToken);
        if (todo is null)
            return Result.Failure(Error.NotFound("Todo.NotFound", $"Todo with ID '{command.Id}' was not found."));

        dbContext.Todos.Remove(todo);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
