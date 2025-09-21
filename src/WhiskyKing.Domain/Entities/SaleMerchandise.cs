namespace WhiskyKing.Domain.Entities;

public class SaleMerchandise : BaseEntityMin
{
    private SaleMerchandise()
    {
    }

    public SaleMerchandise(Guid merchandiseId, decimal quantity, decimal price, string? detail, decimal discount)
    {
        MerchandiseId = merchandiseId;
        Quantity = quantity;
        Price = price;
        Detail = detail;
        Discount = discount;
    }

    public Guid SaleId { get; private set; }
    public Guid MerchandiseId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }
    public string? Detail { get; private set; }
    public decimal Discount { get; private set; }

    public Sale? Sale { get; private set; }
    public Merchandise? Merchandise { get; private set; }
}