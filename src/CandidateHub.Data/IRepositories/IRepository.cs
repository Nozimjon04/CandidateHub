using System.Linq.Expressions;

namespace CandidateHub.Data.IRepositories;

public interface IRepository<TEntity>
{
    public TEntity Update(TEntity entity);
    public Task<bool> SaveChangeAsync(CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null);
    public Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
}
