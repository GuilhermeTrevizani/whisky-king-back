namespace WhiskyKing.Domain.Entities;

public class CategoryDetail : BaseEntityMin
{
    private CategoryDetail()
    {
    }

    public CategoryDetail(string detail)
    {
        Detail = detail;
    }

    public CategoryDetail(Guid categoryId, string detail)
    {
        CategoryId = categoryId;
        Detail = detail;
    }

    public Guid CategoryId { get; private set; }
    public string Detail { get; private set; } = default!;

    public Category? Category { get; private set; }
}