using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class UserRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) : BaseRepository<User>(databaseContext, authenticatedUser), IUserRepository
{
    public Task<User?> GetActiveById(Guid id)
    {
        return _dbSet
            .Include(x => x.UsersAccessGroups!)
                .ThenInclude(x => x.AccessGroup!)
                    .ThenInclude(x => x.AccessGroupsPermissions)
            .FirstOrDefaultAsync(x => !x.DeletedDate.HasValue && x.Id == id);
    }

    public Task<User?> GetActiveByLogin(string login)
    {
        return _dbSet
            .Include(x => x.UsersAccessGroups!)
                .ThenInclude(x => x.AccessGroup!)
                    .ThenInclude(x => x.AccessGroupsPermissions)
            .FirstOrDefaultAsync(x => !x.DeletedDate.HasValue && x.Login == login);
    }

    public Task<User?> GetById(Guid id)
    {
        return _dbSet
            .Include(x => x.UsersAccessGroups)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<User?> GetByLogin(string login)
    {
        return _dbSet
            .FirstOrDefaultAsync(x => x.Login == login);
    }
}