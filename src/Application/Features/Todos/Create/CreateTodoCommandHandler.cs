using EGG.CleanAspire.Domain.Common;
using EGG.CleanAspire.Domain.Entities;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Todos.Create;

public sealed class CreateTodoCommandHandler(IAppDbContext dbContext) : ICommandHandler<CreateTodoCommand, Result<CreateTodoResponse>>
{
    public async ValueTask<Result<CreateTodoResponse>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = new TodoItem
        {
            Title = command.Title,
            Description = command.Description
        };

        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new CreateTodoResponse(todo.Id, todo.Title, todo.Description);
        return Result.Success(response);
    }
}
