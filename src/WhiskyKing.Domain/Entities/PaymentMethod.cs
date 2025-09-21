namespace WhiskyKing.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public PaymentMethod()
    {
    }

    public PaymentMethod(string name)
    {
        Name = name;
    }

    public string Name { get; private set; } = default!;

    public void Update(string name, DateTime? deletedDate)
    {
        Name = name;
        DeletedDate = deletedDate;
    }
}