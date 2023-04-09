using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository
{
  public interface IYerbaMateImageRepository : IRepository<YerbaMateImage>
  {
    void Update(YerbaMateImage image);
    void UpdateRange(IEnumerable<YerbaMateImage> images);
  }
}