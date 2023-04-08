using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class YerbaMateRepository : Repository<YerbaMate>, IYerbaMateRepository
{
  private readonly AppDbContext _dbContext;

  public YerbaMateRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(YerbaMate product)
  {
    var porducFromDb = _dbContext.YerbaMate.FirstOrDefault(p => p.Id == product.Id);
    if (porducFromDb != null)
    {
      porducFromDb.Name = product.Name;
      porducFromDb.Brand = product.Brand;
      porducFromDb.Description = product.Description;
      porducFromDb.Composition = product.Composition;
      porducFromDb.HasAdditives = product.HasAdditives;
      porducFromDb.Weight = product.Weight;
      porducFromDb.Price = product.Price;
      porducFromDb.DiscountPrice = product.DiscountPrice;
    }
  }
}
