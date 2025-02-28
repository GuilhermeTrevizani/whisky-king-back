using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class UserAccessGroupRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) : BaseRepository<UserAccessGroup>(databaseContext, authenticatedUser), IUserAccessGroupRepository
{
}