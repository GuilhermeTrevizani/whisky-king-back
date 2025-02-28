using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Models;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data;

public class DatabaseContext(
    DbContextOptions<DatabaseContext> options,
    IAuthenticatedUser authenticatedUser
        ) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        await InsertAudit();

        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public async Task InsertAudit()
    {
        var audits = new List<Audit>();

        ChangeTracker.DetectChanges();

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;

            var auditEntry = new AuditEntry
            {
                UserId = authenticatedUser.Id ?? Guid.Empty,
                TableName = entry.Metadata.GetTableName() ?? string.Empty,
                EntityState = entry.State,
            };

            foreach (var property in entry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValue = property.CurrentValue?.ToString() ?? string.Empty;
                    continue;
                }

                var propertyName = property.Metadata.Name;

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        if (entry.Entity is BaseEntityMin baseEntityMin)
                            baseEntityMin.SetRegisterUser(auditEntry.UserId);
                        break;
                    case EntityState.Deleted:
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            if (entry.Entity is BaseEntity baseEntity)
                                baseEntity.SetLastChangeUser(auditEntry.UserId);
                        }
                        break;
                }

            }

            audits.Add(auditEntry.ToAudit());
        }

        await AddRangeAsync(audits);
    }
}