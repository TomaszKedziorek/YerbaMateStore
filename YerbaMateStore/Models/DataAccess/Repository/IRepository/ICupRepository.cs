using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;
public interface ICupRepository : IRepository<Cup>
{
  void Update(Cup cup);
}
