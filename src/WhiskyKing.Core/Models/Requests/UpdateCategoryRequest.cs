namespace WhiskyKing.Core.Models.Requests;

public class UpdateCategoryRequest : CreateCategoryRequest
{
    public Guid Id { get; set; }
    public bool Inactive { get; set; }
}