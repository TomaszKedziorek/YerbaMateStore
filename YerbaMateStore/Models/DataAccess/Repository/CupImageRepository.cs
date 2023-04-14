using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.DataAccess.Repository;

public class CupImageRepository : Repository<CupImage>, IImageRepository<CupImage>
{
  private readonly AppDbContext _dbContext;

  public CupImageRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(CupImage image)
  {
    _dbContext.CupImages.Update(image);
  }

  public void UpdateRange(IEnumerable<CupImage> images)
  {
    _dbContext.CupImages.UpdateRange(images);
  }
}
