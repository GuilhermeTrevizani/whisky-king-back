using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Interfaces.Repositories;

public interface ISaleMerchandiseRepository : IBaseRepository<SaleMerchandise>
{
    Task<List<ChartResponse>> Get10CategoriesBestSellers(Guid? shiftId);
    Task<List<ChartResponse>> Get10MerchandisesBestSellers(Guid? shiftId);
}