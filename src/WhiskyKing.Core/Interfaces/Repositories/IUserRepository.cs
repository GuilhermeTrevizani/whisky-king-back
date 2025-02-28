using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetActiveByLogin(string login);
    Task<User?> GetActiveById(Guid id);
    Task<User?> GetById(Guid id);
    Task<User?> GetByLogin(string login);
}