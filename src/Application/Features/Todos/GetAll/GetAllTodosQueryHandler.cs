using EGG.CleanAspire.Application.Features.Todos.Get;
using EGG.CleanAspire.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Mediator;

namespace EGG.CleanAspire.Application.Features.Todos.GetAll;

public sealed class GetAllTodosQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetAllTodosQuery, Result<PagedResult<TodoDetailResponse>>>
{
    public async ValueTask<Result<PagedResult<TodoDetailResponse>>> Handle(GetAllTodosQuery query, CancellationToken cancellationToken)
    {
        var totalCount = await dbContext.Todos.CountAsync(cancellationToken);

        var items = await dbContext.Todos
            .OrderByDescending(t => t.CreatedAt)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(t => new TodoDetailResponse(t.Id, t.Title, t.Description, t.IsCompleted, t.CompletedAt, t.CreatedAt))
            .ToListAsync(cancellationToken);

        var pagedResult = new PagedResult<TodoDetailResponse>(items, totalCount, query.Page, query.PageSize);
        return Result.Success(pagedResult);
    }
}
