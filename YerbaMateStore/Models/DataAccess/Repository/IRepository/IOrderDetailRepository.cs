using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;

public interface IOrderDetailRepository<T> : IRepository<T> where T :OrderDetail, new()
{
  void Update(T orderDetail);
}
