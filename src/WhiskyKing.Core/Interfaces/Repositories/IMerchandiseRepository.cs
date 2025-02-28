using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Interfaces.Repositories;

public interface IMerchandiseRepository : IBaseRepository<Merchandise>
{
    Task<Merchandise?> GetById(Guid id);
}