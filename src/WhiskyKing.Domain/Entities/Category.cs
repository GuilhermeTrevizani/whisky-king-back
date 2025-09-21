namespace WhiskyKing.Domain.Entities;

public class Category : BaseEntity
{
    public Category()
    {
    }

    public Category(string name, ICollection<CategoryDetail> details)
    {
        Name = name;
        Details = details;
    }

    public string Name { get; private set; } = default!;

    public ICollection<Merchandise>? Merchandises { get; private set; }
    public ICollection<CategoryDetail>? Details { get; private set; }

    public void Update(string name, DateTime? deletedDate)
    {
        Name = name;
        DeletedDate = deletedDate;
    }
}