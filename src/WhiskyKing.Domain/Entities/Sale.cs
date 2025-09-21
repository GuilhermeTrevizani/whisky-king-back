namespace WhiskyKing.Domain.Entities;

public class Sale : BaseEntity
{
    private Sale()
    {
    }

    public Sale(Guid shiftId, ICollection<SaleMerchandise> salesMerchandises, ICollection<SalePaymentMethod> salesPaymentMethods)
    {
        ShiftId = shiftId;
        SalesMerchandises = salesMerchandises;
        SalesPaymentMethods = salesPaymentMethods;
    }

    public Guid ShiftId { get; private set; }

    public Shift? Shift { get; private set; }
    public ICollection<SaleMerchandise>? SalesMerchandises { get; private set; }
    public ICollection<SalePaymentMethod>? SalesPaymentMethods { get; private set; }
}