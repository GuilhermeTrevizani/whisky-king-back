namespace WhiskyKing.Domain.Entities;

public class Merchandise : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; }
    public decimal Price { get; private set; }

    public Category? Category { get; private set; }

    public void Create(string name, Guid categoryId, decimal price)
    {
        Name = name;
        CategoryId = categoryId;
        Price = price;
    }

    public void Update(string name, Guid categoryId, decimal price, DateTime? deletedDate)
    {
        Name = name;
        CategoryId = categoryId;
        Price = price;
        DeletedDate = deletedDate;
    }
}