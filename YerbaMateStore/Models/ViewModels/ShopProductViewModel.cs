using System.Linq.Expressions;
using System.Reflection;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.ViewModels;

public class ShopProductViewModel<T> where T : class, new()
{
  private readonly IUnitOfWork _unitOfWork;
  public T Product { get; set; }

  public ShopProductViewModel()
  {
  }

  public ShopProductViewModel(IUnitOfWork unitOfWork, int id)
  {
    _unitOfWork = unitOfWork;

    var param = Expression.Parameter(typeof(T), "e");
    var prop = Expression.Property(param, "Id");
    var value = Expression.Constant(id);
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

    this.Product = (T)result;
  }
}
