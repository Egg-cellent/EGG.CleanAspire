using EGG.CleanAspire.Application.Features.Todos.Delete;
using EGG.CleanAspire.Domain.Common;
using EGG.CleanAspire.Domain.Entities;
using FluentAssertions;

namespace EGG.CleanAspire.Application.UnitTests.Features.Todos;

public sealed class DeleteTodoCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Delete_Todo_When_Found()
    {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();
        var todo = new TodoItem { Title = "Test" };
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var handler = new DeleteTodoCommandHandler(dbContext);

        // Act
        var result = await handler.Handle(new DeleteTodoCommand(todo.Id), TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
        dbContext.Todos.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_Return_NotFound_When_Missing()
    {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();
        var handler = new DeleteTodoCommandHandler(dbContext);

        // Act
        var result = await handler.Handle(new DeleteTodoCommand(Guid.NewGuid()), TestContext.Current.CancellationToken);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error!.Type.Should().Be(ErrorType.NotFound);
    }
}
