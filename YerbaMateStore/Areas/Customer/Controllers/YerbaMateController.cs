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
    ShoppingCartManager<YerbaMate, YerbaMateShoppingCart> manager = new(_unitOfWork);
    YerbaMateShoppingCart shoppingCart = manager.CreateShoppingCart(productId);
    Country country = _unitOfWork.YerbaMate.GetFirstOrDefault(p => p.Id == productId, includeProperties: "Country").Country;
    ViewData["CountryName"] = country.Name;
    ViewData["CountryCode"] = country.CountryIsoAlfa2Code;
    return View(shoppingCart);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Details(YerbaMateShoppingCart shoppingCart)
  {
    if (!ModelState.IsValid)
    {
      YerbaMateProductsViewModel YerbaMateVM = new(_unitOfWork);
      return RedirectToAction(nameof(Index), YerbaMateVM);
    }
    else
    {
      ShoppingCartManager<YerbaMate, YerbaMateShoppingCart> manager = new(_unitOfWork);
      shoppingCart = manager.CreateShoppingCart(shoppingCart.ProductId, shoppingCart.Quantity);
      Country country = _unitOfWork.YerbaMate.GetFirstOrDefault(p => p.Id == shoppingCart.ProductId, includeProperties: "Country").Country;
      ViewData["CountryName"] = country.Name;
      ViewData["CountryCode"] = country.CountryIsoAlfa2Code;
      return View(shoppingCart);
    }
  }
}
