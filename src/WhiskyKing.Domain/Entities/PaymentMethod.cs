namespace WhiskyKing.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public void Create(string name)
    {
        Name = name;
    }

    public void Update(string name, DateTime? deletedDate)
    {
        Name = name;
        DeletedDate = deletedDate;
    }
}