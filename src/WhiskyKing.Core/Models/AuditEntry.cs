using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Models;

public class AuditEntry
{
    public string TableName { get; set; } = string.Empty;
    public EntityState EntityState { get; set; }
    public Guid UserId { get; set; }
    public string KeyValue { get; set; } = string.Empty;
    public Dictionary<string, object?> OldValues { get; } = [];
    public Dictionary<string, object?> NewValues { get; } = [];

    public Audit ToAudit()
    {
        var audit = new Audit();
        audit.Create(TableName, KeyValue, (byte)EntityState, UserId, JsonSerializer.Serialize(OldValues), JsonSerializer.Serialize(NewValues));
        return audit;
    }
}