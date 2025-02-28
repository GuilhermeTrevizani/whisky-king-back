using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Interfaces.Repositories;

public interface IShiftRepository : IBaseRepository<Shift>
{
    Task<Shift?> GetCurrent();
    Task<List<Shift>> GetAll();
}