using EGG.CleanAspire.Api.Extensions;
using EGG.CleanAspire.Application.Features.Todos.Complete;
using EGG.CleanAspire.Application.Features.Todos.Create;
using EGG.CleanAspire.Application.Features.Todos.Delete;
using EGG.CleanAspire.Application.Features.Todos.Get;
using EGG.CleanAspire.Application.Features.Todos.GetAll;
using EGG.CleanAspire.Application.Features.Todos.Update;
using Mediator;

namespace EGG.CleanAspire.Api.Endpoints;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/todos")
            .WithTags("Todos")
            .RequireAuthorization();

        group.MapGet("/", GetAll)
            .WithName("GetAllTodos")
            .WithSummary("Get all todos with pagination");

        group.MapGet("/{id:guid}", GetById)
            .WithName("GetTodoById")
            .WithSummary("Get a todo by ID");

        group.MapPost("/", Create)
            .AddEndpointFilter<ValidationFilter<CreateTodoCommand>>()
            .WithName("CreateTodo")
            .WithSummary("Create a new todo");

        group.MapPut("/{id:guid}", Update)
            .AddEndpointFilter<ValidationFilter<UpdateTodoRequest>>()
            .WithName("UpdateTodo")
            .WithSummary("Update an existing todo");

        group.MapPatch("/{id:guid}/complete", Complete)
            .WithName("CompleteTodo")
            .WithSummary("Mark a todo as completed");

        group.MapDelete("/{id:guid}", Delete)
            .WithName("DeleteTodo")
            .WithSummary("Delete a todo");
    }

    private static async Task<IResult> GetAll(
        int? page,
        int? pageSize,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetAllTodosQuery(page ?? 1, pageSize ?? 10);
        var result = await sender.Send(query, cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
    }

    private static async Task<IResult> GetById(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetTodoQuery(id), cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
    }

    private static async Task<IResult> Create(
        CreateTodoCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? TypedResults.CreatedAtRoute(result.Value, "GetTodoById", new { id = result.Value!.Id })
            : result.ToProblemDetails();
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateTodoRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTodoCommand(id, request.Title, request.Description);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? TypedResults.NoContent() : result.ToProblemDetails();
    }

    private static async Task<IResult> Complete(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CompleteTodoCommand(id), cancellationToken);
        return result.IsSuccess ? TypedResults.NoContent() : result.ToProblemDetails();
    }

    private static async Task<IResult> Delete(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteTodoCommand(id), cancellationToken);
        return result.IsSuccess ? TypedResults.NoContent() : result.ToProblemDetails();
    }
}

public sealed record UpdateTodoRequest(string Title, string? Description);
