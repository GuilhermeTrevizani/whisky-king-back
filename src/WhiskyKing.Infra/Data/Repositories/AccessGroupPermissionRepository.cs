using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class AccessGroupPermissionRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) :
    BaseRepository<AccessGroupPermission>(databaseContext, authenticatedUser), IAccessGroupPermissionRepository
{
}