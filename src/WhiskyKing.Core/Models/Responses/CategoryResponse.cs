namespace WhiskyKing.Core.Models.Responses;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Inactive { get; set; }
    public IEnumerable<string> Details { get; set; } = [];
}