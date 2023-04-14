using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.DataAccess.Repository;

public class BombillaImageRepository : Repository<BombillaImage>, IImageRepository<BombillaImage>
{
  private readonly AppDbContext _dbContext;

  public BombillaImageRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(BombillaImage image)
  {
    _dbContext.BombillaImages.Update(image);
  }

  public void UpdateRange(IEnumerable<BombillaImage> images)
  {
    _dbContext.BombillaImages.UpdateRange(images);
  }
}
