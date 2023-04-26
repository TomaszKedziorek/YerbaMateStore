using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Repository;

public class ShoppingCartRepository<T> : Repository<T>, IShoppingCartRepository<T> where T : ShoppingCart
{
  private readonly AppDbContext _dbContext;
  public ShoppingCartRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public void Update(T shoppingCart)
  {
    var property = _dbContext.GetType().GetProperties()
                  .First(p => p.PropertyType.IsAssignableTo(typeof(DbSet<T>)));
    object obj = property.GetValue(_dbContext);
    object? result = obj.GetType().GetMethod("Update").Invoke(obj, new object[]
    {
        shoppingCart
    });
  }

  public int DecrementQuantity(T shoppingCart, int count)
  {
    shoppingCart.Quantity -= count;
    return shoppingCart.Quantity;
  }

  public int IncrementQuantity(T shoppingCart, int count)
  {
    shoppingCart.Quantity += count;
    return shoppingCart.Quantity;
  }
}
