using System.Linq.Expressions;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Pagination;

namespace YerbaMateStore.Models.ViewModels;

public class YerbaMateProductsViewModel : IProductsViewModel
{
  private readonly IUnitOfWork _unitOfWork;
  public IEnumerable<YerbaMate> ProductList { get; set; }

  public IEnumerable<Country> CountryList { get; set; }
  public IEnumerable<String> BrandList { get; set; }
  public IEnumerable<String> WeigthList { get; set; }
  public string? Filter { get; set; }

  public YerbaMateProductsViewModel(IUnitOfWork unitOfWork, YerbaMateQuery query)
  {
    _unitOfWork = unitOfWork;
    this.CountryList = _unitOfWork.Countries.GetAll();
    this.BrandList = _unitOfWork.YerbaMate.GetAll().Select(p => p.Brand).Distinct();
    this.WeigthList = _unitOfWork.YerbaMate.GetAll().Select(p => p.Weight).Distinct();
    this.ProductList = GetAllWhere(query);
  }
  private IEnumerable<YerbaMate> GetAllWhere(YerbaMateQuery query)
  {
    IEnumerable<YerbaMate> baseQuery = _unitOfWork.YerbaMate.GetAll(TypeFilter(query.type), "Images,Country", false);
    int totalItemsCount = baseQuery.Count();
    IEnumerable<YerbaMate> productList = baseQuery
      .Skip(query.PageSize * (query.PageNumber - 1))
      .Take(query.PageSize)
      .ToList();
    return productList;
  }


  public YerbaMateProductsViewModel(IUnitOfWork unitOfWork,
    string? type = null, string? country = null, string? brand = null, string? weight = null)
  {
    _unitOfWork = unitOfWork;
    this.CountryList = _unitOfWork.Countries.GetAll();
    this.BrandList = _unitOfWork.YerbaMate.GetAll().Select(p => p.Brand).Distinct();
    this.WeigthList = _unitOfWork.YerbaMate.GetAll().Select(p => p.Weight).Distinct();
    this.ProductList = GetAllWhere(type, country, brand, weight);
  }

  private Expression<Func<YerbaMate, bool>> TypeFilter(string type)
  {
    Expression<Func<YerbaMate, bool>> filter;
    switch (type)
    {
      case "all":
        filter = p => p.Id != 0;
        this.Filter = "All";
        break;
      case "classic":
        filter = p => p.HasAdditives == false;
        this.Filter = "Classic";
        break;
      case "additives":
        filter = p => p.HasAdditives == true;
        this.Filter = "With Additives";
        break;
      default:
        filter = p => p.Id != 0;
        this.Filter = "All";
        break;
    }
    return filter;
  }

  private Expression<Func<YerbaMate, bool>> CountryFilter(string country)
  {
    Expression<Func<YerbaMate, bool>> filter = p => p.Country.Name.Contains(country);
    this.Filter = country;
    return filter;
  }
  private Expression<Func<YerbaMate, bool>> BrandFilter(string brand)
  {
    Expression<Func<YerbaMate, bool>> filter = p => p.Brand.Contains(brand);
    this.Filter = brand;
    return filter;
  }
  private Expression<Func<YerbaMate, bool>> WeightFilter(string weight)
  {
    Expression<Func<YerbaMate, bool>> filter = p => p.Weight.Contains(weight);
    this.Filter = weight;
    return filter;
  }

  private IEnumerable<YerbaMate> GetAllWhere(string? type = null, string? country = null, string? brand = null, string? weight = null)
  {
    Expression<Func<YerbaMate, bool>> filter = null;
    if (!string.IsNullOrEmpty(type)) { filter = TypeFilter(type); }
    if (!string.IsNullOrEmpty(country)) { filter = CountryFilter(country); }
    if (!string.IsNullOrEmpty(brand)) { filter = BrandFilter(brand); }
    if (!string.IsNullOrEmpty(weight)) { filter = WeightFilter(weight); }
    if (string.IsNullOrEmpty(this.Filter)) { this.Filter = "All"; }
    IEnumerable<YerbaMate> productList = _unitOfWork.YerbaMate.GetAll(filter, includeProperties: "Images,Country");
    return productList;
  }
}
