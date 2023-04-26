using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class YerbaMateController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public YerbaMateController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  [HttpGet]
  public IActionResult Index(string type, string country, string brand, string weight)
  {
    YerbaMateProductsViewModel YerbaMateVM = new(_unitOfWork, type, country, brand, weight);
    return View(YerbaMateVM);
  }

  [HttpGet]
  public IActionResult Details(int productId)
  {
    ShoppingCartManager<YerbaMate> manager = new(_unitOfWork);
    YerbaMateShoppingCart shoppingCart = new(manager.CreateShoppingCartProduct(productId), productId);
    Country country = _unitOfWork.YerbaMate.GetFirstOrDefault(p => p.Id == productId, includeProperties: "Country").Country;
    ViewData["CountryName"] = country.Name;
    ViewData["CountryCode"] = country.CountryIsoAlfa2Code;
    return View(shoppingCart);
  }

}
