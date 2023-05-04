using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository
{
  public interface IDeliveryMethodRepository : IRepository<DeliveryMethod>
  {
    void Update(DeliveryMethod method);
  }
}