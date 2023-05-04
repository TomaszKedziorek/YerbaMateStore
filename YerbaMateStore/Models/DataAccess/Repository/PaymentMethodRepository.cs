using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
{
  private readonly AppDbContext _dbContext;
  public PaymentMethodRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(PaymentMethod method)
  {
    _dbContext.PaymentMethod.Update(method);
  }
}
