using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.DataAccess.Repository;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;
public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _dbContext;
  public IYerbaMateRepository YerbaMate { get; private set; }
  public ICountryRepository Countries { get; private set; }
  public IYerbaMateImageRepository YerbaMateImage { get; private set; }


  public UnitOfWork(AppDbContext dbContext)
  {
    _dbContext = dbContext;
    YerbaMate = new YerbaMateRepository(dbContext);
    Countries = new CountryRepository(dbContext);
    YerbaMateImage = new YerbaMateImageRepository(dbContext);
  }

  public void Save()
  {
    _dbContext.SaveChanges();
  }
}