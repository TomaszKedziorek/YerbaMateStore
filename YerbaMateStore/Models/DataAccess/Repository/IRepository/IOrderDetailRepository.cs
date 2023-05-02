using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;

public interface IOrderDetailRepository<T> : IRepository<ProductOrderDetail<T>> where T : class, new()
{
  void Update(ProductOrderDetail<T> orderDetail);
}
