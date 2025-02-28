using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Interfaces.Repositories;

public interface ISalePaymentMethodRepository : IBaseRepository<SalePaymentMethod>
{
    Task<List<ChartResponse>> Get10PaymentMethodsMostUsed(Guid? shiftId);
}