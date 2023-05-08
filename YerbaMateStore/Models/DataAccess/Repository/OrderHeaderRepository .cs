using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
  private readonly AppDbContext _dbContext;
  public OrderHeaderRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(OrderHeader orderHeader)
  {
    _dbContext.OrderHeader.Update(orderHeader);
  }

  public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
  {
    OrderHeader? orderFromDb = _dbContext.OrderHeader.FirstOrDefault(u => u.Id == id);
    if (orderFromDb != null)
    {
      orderFromDb.OrderStatus = orderStatus;
      if (orderFromDb.PaymentStatus != null)
      {
        orderFromDb.PaymentStatus = paymentStatus;
      }
    }
  }

  public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
  {
    OrderHeader orderFromDb = _dbContext.OrderHeader.FirstOrDefault(o => o.Id == id);
    orderFromDb.SessionId = sessionId;
    orderFromDb.PaymentIntentId = paymentIntentId;
  }
}