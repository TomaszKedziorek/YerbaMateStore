using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class BombillaRepository : Repository<Bombilla>, IBombillaRepository
{
  private readonly AppDbContext _dbContext;

  public BombillaRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(Bombilla product)
  {
    var porducFromDb = _dbContext.Bombilla.FirstOrDefault(p => p.Id == product.Id);
    if (porducFromDb != null)
    {
      porducFromDb.Name = product.Name;
      porducFromDb.Description = product.Description;
      porducFromDb.Material = product.Material;
      porducFromDb.IsUnscrewed = product.IsUnscrewed;
      porducFromDb.Length = product.Length;
      porducFromDb.Price = product.Price;
      porducFromDb.DiscountPrice = product.DiscountPrice;
      porducFromDb.Images = product.Images;
    }
  }
}
