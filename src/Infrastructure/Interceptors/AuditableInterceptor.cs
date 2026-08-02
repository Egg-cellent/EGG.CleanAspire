using EGG.CleanAspire.Application.Abstractions.Identity;
using EGG.CleanAspire.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EGG.CleanAspire.Infrastructure.Interceptors;

public sealed class AuditableInterceptor(ICurrentUser currentUser, TimeProvider dateTime) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAudit(DbContext? context)
    {
        if (context is null) return;

        // Reading ChangeTracker.Entries<T>() already forces change detection
        // when auto-detect is on (the default), but calling DetectChanges
        // explicitly keeps auditing correct even if a code path turned
        // auto-detect off. It's what EF Core's own audit sample does.
        context.ChangeTracker.DetectChanges();

        var now = dateTime.GetUtcNow();
        var user = currentUser.UserId ?? "system";

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.CreatedBy = user;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedAt = now;
                entry.Entity.LastModifiedBy = user;
            }
        }
    }
}
