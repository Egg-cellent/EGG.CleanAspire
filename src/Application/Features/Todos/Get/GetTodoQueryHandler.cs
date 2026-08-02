using EGG.CleanAspire.Domain.Common;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Todos.Get;

public sealed class GetTodoQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetTodoQuery, Result<TodoDetailResponse>>
{
    public async ValueTask<Result<TodoDetailResponse>> Handle(GetTodoQuery query, CancellationToken cancellationToken)
    {
        var todo = await dbContext.Todos.FindAsync([query.Id], cancellationToken);
        if (todo is null)
            return Result.Failure<TodoDetailResponse>(Error.NotFound("Todo.NotFound", $"Todo with ID '{query.Id}' was not found."));

        var response = new TodoDetailResponse(todo.Id, todo.Title, todo.Description, todo.IsCompleted, todo.CompletedAt, todo.CreatedAt);
        return Result.Success(response);
    }
}
