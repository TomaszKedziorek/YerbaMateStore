using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;

public interface IShoppingCartRepository<T> : IRepository<T> where T : ShoppingCart
{
  void Update(T shoppingCart);
  int DecrementQuantity(T shoppingCart, int count);
  int IncrementQuantity(T shoppingCart, int count);
}