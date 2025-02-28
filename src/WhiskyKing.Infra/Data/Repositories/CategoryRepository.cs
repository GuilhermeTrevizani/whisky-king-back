using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class CategoryRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) :
    BaseRepository<Category>(databaseContext, authenticatedUser), ICategoryRepository
{
    public Task<Category?> GetById(Guid id)
    {
        return _dbSet.Include(x => x.Details).FirstOrDefaultAsync(x => x.Id == id);
    }
}