using EGG.CleanAspire.Domain.Common;

namespace EGG.CleanAspire.Application.Features.Todos.Get;

public sealed record GetTodoQuery(Guid Id) : IQuery<Result<TodoDetailResponse>>;

public sealed record TodoDetailResponse(Guid Id, string Title, string? Description, bool IsCompleted, DateTimeOffset? CompletedAt, DateTimeOffset CreatedAt);
