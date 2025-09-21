using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Models;

public class AuditEntry
{
    public string TableName { get; set; } = default!;
    public EntityState EntityState { get; set; }
    public Guid UserId { get; set; }
    public string KeyValue { get; set; } = default!;
    public Dictionary<string, object?> OldValues { get; } = [];
    public Dictionary<string, object?> NewValues { get; } = [];

    public Audit ToAudit()
    {
        var audit = new Audit(TableName, KeyValue, (byte)EntityState, UserId, JsonSerializer.Serialize(OldValues), JsonSerializer.Serialize(NewValues));
        return audit;
    }
}