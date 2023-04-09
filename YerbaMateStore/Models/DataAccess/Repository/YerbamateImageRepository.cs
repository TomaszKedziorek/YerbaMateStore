using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.DataAccess.Repository;

public class YerbaMateImageRepository : Repository<YerbaMateImage>, IYerbaMateImageRepository
{
  private readonly AppDbContext _dbContext;

  public YerbaMateImageRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(YerbaMateImage image)
  {
    _dbContext.YerbaMateImages.Update(image);
  }

  public void UpdateRange(IEnumerable<YerbaMateImage> images)
  {
    _dbContext.YerbaMateImages.UpdateRange(images);
  }
}
