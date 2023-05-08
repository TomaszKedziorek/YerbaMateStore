using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class DeliveryMethodRepository : Repository<DeliveryMethod>, IDeliveryMethodRepository
{
  private readonly AppDbContext _dbContext;
  public DeliveryMethodRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(DeliveryMethod method)
  {
    _dbContext.DeliveryMethod.Update(method);
  }
}
