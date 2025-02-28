using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DatabaseContext _databaseContext;
    protected readonly IAuthenticatedUser _authenticatedUser;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(
        DatabaseContext databaseContext,
        IAuthenticatedUser authenticatedUser
        )
    {
        _databaseContext = databaseContext;
        _authenticatedUser = authenticatedUser;
        _dbSet = _databaseContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _databaseContext.AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _databaseContext.AddRangeAsync(entities);
        return entities;
    }

    public void Delete(TEntity entity)
    {
        _databaseContext.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            Delete(entity);
    }

    public void Update(TEntity entity)
    {
        _databaseContext.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _databaseContext.UpdateRange(entities);
    }

    public async Task<PaginationRepositoryResponse<TEntity>> GetPagination(PaginationRepositoryRequest<TEntity> request)
    {
        var query = _databaseContext.Set<TEntity>().AsQueryable().AsNoTracking();

        if (request.WhereExpression != null)
            query = query.Where(request.WhereExpression);

        if (request.OnlyActive)
        {
            var parameterExpression = Expression.Parameter(typeof(TEntity), "p");
            var expression = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(
                    Expression.Property(parameterExpression, nameof(BaseEntity.DeletedDate)),
                    Expression.Constant(null)
                ),
                parameterExpression
            );

            query = query.Where(expression);
        }

        foreach (var include in request.Includes)
            query = query.Include(include);

        var response = new PaginationRepositoryResponse<TEntity>
        {
            TotalCount = await query.CountAsync(),
        };

        query = request.OrderDescending ? query.OrderByDescending(request.OrderExpression) : query.OrderBy(request.OrderExpression);

        response.Data = await query.Skip(request.Skip).Take(request.Take).ToListAsync();

        return response;
    }
}