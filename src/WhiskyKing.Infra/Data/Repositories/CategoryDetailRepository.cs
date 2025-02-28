using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class CategoryDetailRepository(DatabaseContext databaseContext, IAuthenticatedUser authenticatedUser) :
    BaseRepository<CategoryDetail>(databaseContext, authenticatedUser), ICategoryDetailRepository
{
}