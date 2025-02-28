using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class AuditRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) :
    BaseRepository<Audit>(databaseContext, authenticatedUser), IAuditRepository
{
}