using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository;
public class OrderDetailRepository<T> : Repository<ProductOrderDetail<T>>, IOrderDetailRepository<T> where T : class, new()
{
  private readonly AppDbContext _dbContext;
  public OrderDetailRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(ProductOrderDetail<T> orderDetail)
  {
    _dbContext.OrderDetail.Update(orderDetail);
  }
}