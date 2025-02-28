namespace WhiskyKing.Domain.Entities;

public class CategoryDetail : BaseEntityMin
{
    public Guid CategoryId { get; private set; }
    public string Detail { get; private set; } = string.Empty;

    public Category? Category { get; private set; }

    public void Create(string detail)
    {
        Detail = detail;
    }

    public void Create(Guid categoryId, string detail)
    {
        CategoryId = categoryId;
        Detail = detail;
    }
}