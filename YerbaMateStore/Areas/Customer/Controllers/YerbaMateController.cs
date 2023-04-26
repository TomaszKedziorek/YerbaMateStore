using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
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
    if (ModelState.IsValid)
    {
      ClaimsIdentity? claimsIdentity = (ClaimsIdentity)User.Identity;
      Claim? claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
      shoppingCart.ApplicationUserId = claim.Value;
      _unitOfWork.YerbaMateShoppingCart.Add(shoppingCart);
      _unitOfWork.Save();

      TempData["success"] = "Product added to cart!";
      return RedirectToAction(nameof(Index));
    }
    else
    {
      TempData["error"] = "Something went wrong!";
      return RedirectToAction(nameof(Details), new { productId = shoppingCart.ProductId });
    }
  }
}
