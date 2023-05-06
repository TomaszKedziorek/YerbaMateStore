using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository;
public class OrderDetailRepository<T> : Repository<T>, IOrderDetailRepository<T> where T : OrderDetail, new()
{
  private readonly AppDbContext _dbContext;
  public OrderDetailRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(T orderDetail)
  {
    _dbContext.OrderDetail.Update(orderDetail);
  }

  public void AddRange(IEnumerable<T> orderDetailList)
  {
    _dbContext.OrderDetail.AddRange(orderDetailList);
  }
}