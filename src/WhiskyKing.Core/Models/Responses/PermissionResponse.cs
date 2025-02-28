using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Core.Models.Responses;

public class PermissionResponse
{
    public Permission Id { get; set; }
    public string Name { get; set; } = default!;
}