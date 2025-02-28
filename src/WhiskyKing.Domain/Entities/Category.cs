namespace WhiskyKing.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public ICollection<Merchandise>? Merchandises { get; private set; }
    public ICollection<CategoryDetail>? Details { get; private set; }

    public void Create(string name, ICollection<CategoryDetail> details)
    {
        Name = name;
        Details = details;
    }

    public void Update(string name, DateTime? deletedDate)
    {
        Name = name;
        DeletedDate = deletedDate;
    }
}