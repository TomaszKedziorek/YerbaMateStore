using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;
public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _dbContext;

  public UnitOfWork(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public void Save()
  {
    _dbContext.SaveChanges();
  }
}