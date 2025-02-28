using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class PaymentMethodRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) : BaseRepository<PaymentMethod>(databaseContext, authenticatedUser), IPaymentMethodRepository
{
    public Task<PaymentMethod?> GetById(Guid id)
    {
        return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }
}