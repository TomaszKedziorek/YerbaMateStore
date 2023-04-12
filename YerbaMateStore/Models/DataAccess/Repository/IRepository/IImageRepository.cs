using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository
{
  public interface IImageRepository<T>: IRepository<T>  where T:class 
  {
    void Update(T image);
    void UpdateRange(IEnumerable<T> images);
  }
}