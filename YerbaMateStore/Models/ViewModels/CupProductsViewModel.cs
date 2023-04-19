using System.Linq.Expressions;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.ViewModels;

public class CupProductsViewModel
{
  private readonly IUnitOfWork _unitOfWork;
  public IEnumerable<Cup> ProductList { get; set; }
  public IEnumerable<String> VolumeList { get; set; }
  public IEnumerable<String> MaterialList { get; set; }
  public string? Filter { get; set; }

  public CupProductsViewModel(IUnitOfWork unitOfWork,
    string? type = null, string? value = null, string? material = null)
  {
    _unitOfWork = unitOfWork;
    this.VolumeList = _unitOfWork.Cup.GetAll().Select(p => p.Volume).Distinct();
    this.MaterialList = _unitOfWork.Cup.GetAll().Select(p => p.Material).Distinct();
    this.ProductList = GetAllWhere(type, value, material);
  }
  private Expression<Func<Cup, bool>> TypeFilter(string type)
  {
    Expression<Func<Cup, bool>> filter;
    switch (type)
    {
      case "all":
        filter = p => p.Id != 0;
        this.Filter = "All";
        break;
      default:
        filter = p => p.Id != 0;
        this.Filter = "All";
        break;
    }
    return filter;
  }

  private Expression<Func<Cup, bool>> VolumeFilter(string volume)
  {
    Expression<Func<Cup, bool>> filter = p => p.Volume.Contains(volume);
    this.Filter = volume;
    return filter;
  }
  private Expression<Func<Cup, bool>> MaterialFilter(string material)
  {
    Expression<Func<Cup, bool>> filter = p => p.Material.Contains(material);
    this.Filter = material;
    return filter;
  }

  private IEnumerable<Cup> GetAllWhere(string? type = null, string? volume = null, string? material = null)
  {
    Expression<Func<Cup, bool>> filter = null;
    if (!string.IsNullOrEmpty(type)) { filter = TypeFilter(type); }
    if (!string.IsNullOrEmpty(volume)) { filter = VolumeFilter(volume); }
    if (!string.IsNullOrEmpty(material)) { filter = MaterialFilter(material); }
    if (string.IsNullOrEmpty(this.Filter)) { this.Filter = "All"; }
    IEnumerable<Cup> productList = _unitOfWork.Cup.GetAll(filter, includeProperties: "Images");
    return productList;
  }
}
