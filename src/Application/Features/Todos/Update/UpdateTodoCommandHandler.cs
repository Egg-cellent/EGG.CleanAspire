using EGG.CleanAspire.Domain.Common;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Todos.Update;

public sealed class UpdateTodoCommandHandler(IAppDbContext dbContext) : ICommandHandler<UpdateTodoCommand, Result>
{
    public async ValueTask<Result> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await dbContext.Todos.FindAsync([command.Id], cancellationToken);
        if (todo is null)
            return Result.Failure(Error.NotFound("Todo.NotFound", $"Todo with ID '{command.Id}' was not found."));

        todo.Title = command.Title;
        todo.Description = command.Description;

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
