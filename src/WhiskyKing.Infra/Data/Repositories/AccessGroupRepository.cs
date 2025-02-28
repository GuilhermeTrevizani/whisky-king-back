using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class AccessGroupRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) :
    BaseRepository<AccessGroup>(databaseContext, authenticatedUser), IAccessGroupRepository
{
    public Task<AccessGroup?> GetById(Guid id)
    {
        return _dbSet.Include(x => x.AccessGroupsPermissions).FirstOrDefaultAsync(x => x.Id == id);
    }
}