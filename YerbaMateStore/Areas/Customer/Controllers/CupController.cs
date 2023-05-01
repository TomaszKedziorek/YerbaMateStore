using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class CupController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ShoppingCartManager<Cup, CupShoppingCart> _shoppingCartManager;

  public CupController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
    _shoppingCartManager = new(unitOfWork);
  }

  [HttpGet]
  public IActionResult Index(string type, string volume, string material)
  {
    CupProductsViewModel CupVM = new(_unitOfWork, type, volume, material);
    return View(CupVM);
  }

  [HttpGet]
  public IActionResult Details(int productId)
  {
    CupShoppingCart shoppingCart = _shoppingCartManager.CreateShoppingCart(productId);
    return View(shoppingCart);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize]
  public IActionResult Details(CupShoppingCart shoppingCart)
  {
    if (ModelState.IsValid)
    {
      string userClaimsValue = UserClaims.GetUserClaimsValue(User);
      shoppingCart.ApplicationUserId = userClaimsValue;

      var shoppingCartFromDb = _unitOfWork.CupShoppingCart.GetFirstOrDefault(
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
  [Authorize]
  public IActionResult AddToCart(int productId)
  {
    CupShoppingCart shoppingCart = _shoppingCartManager.CreateShoppingCart(productId, 1);
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    shoppingCart.ApplicationUserId = userClaimsValue;
    if (shoppingCart.Product != null)
    {
      var shoppingCartFromDb = _unitOfWork.CupShoppingCart.GetFirstOrDefault(
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
