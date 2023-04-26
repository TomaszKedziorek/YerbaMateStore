using System.Linq.Expressions;
using System.Reflection;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.Managers;
public class ShoppingCartManager<T, F> where T : class, new() where F : ShoppingCart, new()
{
  private readonly IUnitOfWork _unitOfWork;

  public ShoppingCartManager(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public T CreateShoppingCartProduct(int productId)
  {
    var param = Expression.Parameter(typeof(T), "e");
    var prop = Expression.Property(param, "Id");
    var value = Expression.Constant(productId);
    var body = Expression.Equal(prop, value);
    var lambda = Expression.Lambda<Func<T, bool>>(body, param);

    PropertyInfo property = _unitOfWork.GetType().GetProperties()
                  .First(p => p.PropertyType.IsAssignableTo(typeof(IRepository<T>)));
    object obj = property.GetValue(_unitOfWork);
    object? result = obj.GetType().GetMethod("GetFirstOrDefault").Invoke(obj, new object[]
    {
        lambda,
        "Images",
        true
    });
    return (T)result;
  }

  public F CreateShoppingCart(int productId, int quantity = 1)
  {
    F shoppingCart = new();
    PropertyInfo[] properties = shoppingCart.GetType().GetProperties();
    var scProductId = properties.First(p => p.Name == "ProductId");
    var scProduct = properties.First(p => p.Name == "Product");
    scProductId.SetValue(shoppingCart, productId);
    scProduct.SetValue(shoppingCart, CreateShoppingCartProduct(productId));
    shoppingCart.Quantity = quantity;

    return shoppingCart;
  }
}
