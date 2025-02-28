using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class SaleRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) : BaseRepository<Sale>(databaseContext, authenticatedUser), ISaleRepository
{
    public Task<Sale?> GetById(Guid id)
    {
        return _dbSet
            .Include(x => x.SalesMerchandises)
                !.ThenInclude(x => x.Merchandise)
            !.Include(x => x.SalesPaymentMethods)
                !.ThenInclude(x => x.PaymentMethod)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}