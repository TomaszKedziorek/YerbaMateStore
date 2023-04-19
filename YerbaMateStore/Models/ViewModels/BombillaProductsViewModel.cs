using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.ViewModels;

public class BombillaProductsViewModel
{
  private readonly IUnitOfWork _unitOfWork;
  public IEnumerable<Bombilla> ProductList { get; set; }
  public IEnumerable<String> LengthList { get; set; }
  public IEnumerable<String> MaterialList { get; set; }
  public string? Filter { get; set; }

  public BombillaProductsViewModel(IUnitOfWork unitOfWork,
    string? type = null, string? length = null, string? material = null)
  {
    _unitOfWork = unitOfWork;
    this.LengthList = _unitOfWork.Bombilla.GetAll().Select(p => p.Length).Distinct();
    this.MaterialList = _unitOfWork.Bombilla.GetAll().Select(p => p.Material).Distinct();
    this.ProductList = GetAllWhere(type, length, material);
  }
  private Expression<Func<Bombilla, bool>> TypeFilter(string type)
  {
    Expression<Func<Bombilla, bool>> filter;
    switch (type)
    {
      case "all":
        filter = p => p.Id != 0;
        this.Filter = "All";
        break;
      case "unscrewed":
        filter = p => p.IsUnscrewed == true;
        this.Filter = "Unscrewed";
        break;
      case "noUnscrewed":
        filter = p => p.IsUnscrewed == false;
        this.Filter = "Not Unscrewed";
        break;
      default:
        filter = p => p.Id != 0;
        this.Filter = "All";
        break;
    }
    return filter;
  }

  private Expression<Func<Bombilla, bool>> LengthFilter(string length)
  {
    Expression<Func<Bombilla, bool>> filter = p => p.Length.Contains(length);
    this.Filter = length;
    return filter;
  }
  private Expression<Func<Bombilla, bool>> MaterialFilter(string material)
  {
    Expression<Func<Bombilla, bool>> filter = p => p.Material.Contains(material);
    this.Filter = material;
    return filter;
  }

  private IEnumerable<Bombilla> GetAllWhere(string? type = null, string? length = null, string? material = null)
  {
    Expression<Func<Bombilla, bool>> filter = null;
    if (!string.IsNullOrEmpty(type)) { filter = TypeFilter(type); }
    if (!string.IsNullOrEmpty(length)) { filter = LengthFilter(length); }
    if (!string.IsNullOrEmpty(material)) { filter = MaterialFilter(material); }
    if (string.IsNullOrEmpty(this.Filter)) { this.Filter = "All"; }
    IEnumerable<Bombilla> productList = _unitOfWork.Bombilla.GetAll(filter, includeProperties: "Images");
    return productList;
  }
}
