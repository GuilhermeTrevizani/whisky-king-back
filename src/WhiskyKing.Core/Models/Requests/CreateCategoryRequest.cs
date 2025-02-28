namespace WhiskyKing.Core.Models.Requests;

public class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<string> Details { get; set; } = [];
}