namespace WhiskyKing.Core.Models.Requests;

public class CreateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public IEnumerable<Guid> AccessGroups { get; set; } = [];
}