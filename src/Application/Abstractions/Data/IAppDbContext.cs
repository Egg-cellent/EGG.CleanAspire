using EGG.CleanAspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGG.CleanAspire.Application.Abstractions.Data;

public interface IAppDbContext
{
    DbSet<TodoItem> Todos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
