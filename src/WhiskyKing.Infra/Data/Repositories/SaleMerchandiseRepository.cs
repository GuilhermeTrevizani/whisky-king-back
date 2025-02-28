using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class SaleMerchandiseRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) : BaseRepository<SaleMerchandise>(databaseContext, authenticatedUser), ISaleMerchandiseRepository
{
    public Task<List<ChartResponse>> Get10CategoriesBestSellers(Guid? shiftId)
    {
        return _dbSet
            .Where(x => shiftId == null || x.Sale.ShiftId == shiftId)
            .Where(x => !x.Sale.DeletedDate.HasValue)
            .Include(x => x.Sale)
            .Include(x => x.Merchandise)
                    .ThenInclude(x => x.Category)
            .GroupBy(x => x.Merchandise.Category)
            .Select(x => new ChartResponse
            {
                Label = x.Key.Name,
                Value = x.Sum(y => y.Quantity * y.Price),
            })
            .OrderByDescending(x => x.Value)
            .Take(10)
            .ToListAsync();
    }

    public Task<List<ChartResponse>> Get10MerchandisesBestSellers(Guid? shiftId)
    {
        return _dbSet
            .Where(x => shiftId == null || x.Sale.ShiftId == shiftId)
            .Where(x => !x.Sale.DeletedDate.HasValue)
            .Include(x => x.Sale)
            .Include(x => x.Merchandise)
            .GroupBy(x => x.Merchandise)
            .Select(x => new ChartResponse
            {
                Label = x.Key.Name,
                Value = x.Sum(y => y.Quantity * y.Price),
            })
            .OrderByDescending(x => x.Value)
            .Take(10)
            .ToListAsync();
    }
}