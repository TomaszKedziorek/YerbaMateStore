using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;
public interface IYerbaMateRepository : IRepository<YerbaMate>
{
  void Update(YerbaMate yerbaMate);
}
