using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository
{
  public interface IPaymentMethodRepository : IRepository<PaymentMethod>
  {
    void Update(PaymentMethod method);
  }
}