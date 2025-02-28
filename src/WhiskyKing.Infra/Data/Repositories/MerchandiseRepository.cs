using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class MerchandiseRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) : BaseRepository<Merchandise>(databaseContext, authenticatedUser), IMerchandiseRepository
{
    public Task<Merchandise?> GetById(Guid id)
    {
        return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }
}