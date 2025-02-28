using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Core.Models.Responses;

public class AccessGroupResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Permission> Permissions { get; set; } = [];
    public bool Inactive { get; set; }
}