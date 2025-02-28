using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Interfaces.Repositories;

public interface IAccessGroupRepository : IBaseRepository<AccessGroup>
{
    Task<AccessGroup?> GetById(Guid id);
}