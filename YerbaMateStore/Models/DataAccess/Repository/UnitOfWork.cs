using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.DataAccess.Repository;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;
public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _dbContext;
  public IYerbaMateRepository YerbaMate { get; private set; }
  public ICountryRepository Countries { get; private set; }
  public IImageRepository<YerbaMateImage> YerbaMateImage { get; private set; }

  public IBombillaRepository Bombilla { get; private set; }

  public IImageRepository<BombillaImage> BombillaImage { get; private set; }
  public ICupRepository Cup { get; private set; }

  public IImageRepository<CupImage> CupImage { get; private set; }

  public UnitOfWork(AppDbContext dbContext)
  {
    _dbContext = dbContext;
    YerbaMate = new YerbaMateRepository(dbContext);
    Countries = new CountryRepository(dbContext);
    YerbaMateImage = new YerbaMateImageRepository(dbContext);
    Bombilla = new BombillaRepository(dbContext);
    BombillaImage = new BombillaImageRepository(dbContext);
    Cup = new CupRepository(dbContext);
    CupImage = new CupImageRepository(dbContext);
  }

  public void Save()
  {
    _dbContext.SaveChanges();
  }
}