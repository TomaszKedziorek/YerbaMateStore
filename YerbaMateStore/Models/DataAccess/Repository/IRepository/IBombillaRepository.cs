using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;
public interface IBombillaRepository : IRepository<Bombilla>
{
  void Update(Bombilla bombilla);
}
