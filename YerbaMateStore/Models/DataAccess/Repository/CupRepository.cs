using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class CupRepository : Repository<Cup>, ICupRepository
{
  private readonly AppDbContext _dbContext;

  public CupRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(Cup product)
  {
    var porducFromDb = _dbContext.Cup.FirstOrDefault(p => p.Id == product.Id);
    if (porducFromDb != null)
    {
      porducFromDb.Name = product.Name;
      porducFromDb.Description = product.Description;
      porducFromDb.Material = product.Material;
      porducFromDb.Volume = product.Volume;
      porducFromDb.Price = product.Price;
      porducFromDb.DiscountPrice = product.DiscountPrice;
      porducFromDb.Images = product.Images;
    }
  }
}
