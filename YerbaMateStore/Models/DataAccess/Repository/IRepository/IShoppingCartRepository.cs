using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.Repository.IRepository;

public interface IShoppingCartRepository<T> : IRepository<T> where T : ShoppingCart
{
  void Update(T shoppingCart);
}