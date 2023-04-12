using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Models.ViewModels;

public class ProductViewModel<T> where T : class, new()
{
  private readonly IUnitOfWork _unitOfWork;
  public T Product { get; set; }

  [ValidateNever]
  public IEnumerable<SelectListItem> CountryList { get; set; }
  public ProductViewModel()
  {
  }

  public ProductViewModel(IUnitOfWork unitOfWork, T? product = null, int? id = null)
  {
    _unitOfWork = unitOfWork;
    this.CountryList = _unitOfWork.Countries.GetAll().Select(c => new SelectListItem
    {
      Text = c.Name,
      Value = c.Id.ToString(),
    });
    if (id == null || id == 0)
    {
      this.Product = product == null ? new T() : product;
    }
    else
    {
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
}
