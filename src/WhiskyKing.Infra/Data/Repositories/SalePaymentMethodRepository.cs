using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class SalePaymentMethodRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) : BaseRepository<SalePaymentMethod>(databaseContext, authenticatedUser), ISalePaymentMethodRepository
{
    public Task<List<ChartResponse>> Get10PaymentMethodsMostUsed(Guid? shiftId)
    {
        return _dbSet
            .Where(x => shiftId == null || x.Sale.ShiftId == shiftId)
            .Where(x => !x.Sale.DeletedDate.HasValue)
            .Include(x => x.Sale)
            .Include(x => x.PaymentMethod)
            .GroupBy(x => x.PaymentMethod)
            .Select(x => new ChartResponse
            {
                Label = x.Key.Name,
                Value = x.Sum(y => y.Value),
            })
            .OrderByDescending(x => x.Value)
            .Take(10)
            .ToListAsync();
    }
}