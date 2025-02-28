using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Interfaces.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<Sale?> GetById(Guid id);
}