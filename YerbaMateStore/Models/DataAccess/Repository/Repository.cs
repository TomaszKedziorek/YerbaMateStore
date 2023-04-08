using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class Repository<T> : IRepository<T> where T : class
{
  private readonly AppDbContext _dbContext;
  internal DbSet<T> dbSet;

  public Repository(AppDbContext dbContext)
  {
    _dbContext = dbContext;
    this.dbSet = _dbContext.Set<T>();
  }


  public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
  {
    IQueryable<T> query;
    Track(out query, tracked);
    query = query.Where(filter);
    IncludeProperties(ref query, includeProperties);
    return query.FirstOrDefault();
  }

  public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = true)
  {
    IQueryable<T> query;
    Track(out query, tracked);
    if (filter != null)
      query = query.Where(filter);
    IncludeProperties(ref query, includeProperties);
    return query.ToList();
  }

  public void Add(T entity)
  {
    dbSet.Add(entity);
  }

  public void Remove(T entity)
  {
    dbSet.Remove(entity);
  }

  public void RemoveRange(IEnumerable<T> entities)
  {
    dbSet.RemoveRange(entities);
  }

  private void Track(out IQueryable<T> query, bool tracked)
  {
    if (tracked)
    { query = dbSet; }
    else
    { query = dbSet.AsNoTracking(); }
  }

  private void IncludeProperties(ref IQueryable<T> query, string? includeProperties = null)
  {
    if (includeProperties != null)
    {
      foreach (var includProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
      {
        query = query.Include(includProp);
      }
    }
  }
}
