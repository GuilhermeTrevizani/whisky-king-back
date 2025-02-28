using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class ShiftRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) : BaseRepository<Shift>(databaseContext, authenticatedUser), IShiftRepository
{
    public Task<Shift?> GetCurrent()
    {
        return _dbSet.FirstOrDefaultAsync(x => !x.LastChangeDate.HasValue);
    }

    public Task<List<Shift>> GetAll()
    {
        return _dbSet.OrderByDescending(x => x.RegisterDate).ToListAsync();
    }
}