using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;
public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _dbContext;
  public IYerbaMateRepository YerbaMate { get; private set; }


  public UnitOfWork(AppDbContext dbContext)
  {
    _dbContext = dbContext;
    YerbaMate = new YerbaMateRepository(dbContext);
  }

  public void Save()
  {
    _dbContext.SaveChanges();
  }
}