using System.Linq.Expressions;
using CandidateHub.Domain.Commons;
using CandidateHub.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using CandidateHub.Data.IRepositories;

namespace CandidateHub.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly DbSet<TEntity> dbSet;
    private readonly AppDbContext dbContext;

    public Repository(AppDbContext dbContext, DbSet<TEntity> dbSet)
    {
        this.dbSet = dbSet;
        this.dbContext = dbContext;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await this.dbSet.FirstOrDefaultAsync(e => e.Id == id);
        dbSet.Remove(entity);
        return true;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
         => (await this.dbSet.AddAsync(entity)).Entity;
    

    public async Task<bool> SaveChangeAsync(CancellationToken cancellationToken = default)
         => (await this.dbContext.SaveChangesAsync() > 0);

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null)
    {
        var query = expression is null ? dbSet : dbSet.Where(expression);
        return query;
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => await this.SelectAll(expression).FirstOrDefaultAsync();

    public TEntity Update(TEntity entity)
        => this.dbSet.Update(entity).Entity;
}
