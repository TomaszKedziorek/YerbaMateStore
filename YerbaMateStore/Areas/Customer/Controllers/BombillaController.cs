using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class BombillaController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ShoppingCartManager<Bombilla, BombillaShoppingCart> _shoppingCartManager;

  public BombillaController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
    _shoppingCartManager = new(unitOfWork);
  }

  [HttpGet]
  public IActionResult Index(string type, string length, string material)
  {
    BombillaProductsViewModel BombillaVM = new(_unitOfWork, type, length, material);
    return View(BombillaVM);
  }

  [HttpGet]
  public IActionResult Details(int productId)
  {
    BombillaShoppingCart shoppingCart = _shoppingCartManager.CreateShoppingCart(productId);
    return View(shoppingCart);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Details(BombillaShoppingCart shoppingCart)
  {
    if (ModelState.IsValid)
    {
      string userClaimsValue = UserClaims.GetUserClaimsValue(User);
      shoppingCart.ApplicationUserId = userClaimsValue;

      var shoppingCartFromDb = _unitOfWork.BombillaShoppingCart.GetFirstOrDefault(
          i => i.ApplicationUserId == userClaimsValue && i.ProductId == shoppingCart.ProductId);
      _shoppingCartManager.AddOrIncrement(shoppingCart, shoppingCartFromDb);
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

  [HttpPost]
  public IActionResult AddToCart(int productId)
  {
    BombillaShoppingCart shoppingCart = _shoppingCartManager.CreateShoppingCart(productId, 1);
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    shoppingCart.ApplicationUserId = userClaimsValue;
    if (shoppingCart.Product != null)
    {
      var shoppingCartFromDb = _unitOfWork.BombillaShoppingCart.GetFirstOrDefault(
          i => i.ApplicationUserId == userClaimsValue && i.ProductId == shoppingCart.ProductId);
      _shoppingCartManager.AddOrIncrement(shoppingCart, shoppingCartFromDb);
      _unitOfWork.Save();

      return Json(new { success = true, message = "Product added to cart!" });
    }
    else
    {
      return Json(new { success = false, message = "Something went wrong!" });
    }
  }
}
