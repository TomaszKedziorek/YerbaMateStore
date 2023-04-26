using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class BombillaController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public BombillaController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
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
    ShoppingCartManager<Bombilla, BombillaShoppingCart> manager = new(_unitOfWork);
    BombillaShoppingCart shoppingCart = manager.CreateShoppingCart(productId);
    return View(shoppingCart);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Details(BombillaShoppingCart shoppingCart)
  {
    if (ModelState.IsValid)
    {
      ClaimsIdentity? claimsIdentity = (ClaimsIdentity)User.Identity;
      Claim? claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
      shoppingCart.ApplicationUserId = claim.Value;

      var shoppingCartFromDb = _unitOfWork.BombillaShoppingCart.GetFirstOrDefault(i => i.ApplicationUserId == claim.Value && i.ProductId == shoppingCart.ProductId);
      if (shoppingCartFromDb == null)
      {
        _unitOfWork.BombillaShoppingCart.Add(shoppingCart);
      }
      else
      {
        _unitOfWork.BombillaShoppingCart.IncrementQuantity(shoppingCartFromDb, shoppingCart.Quantity);
      }

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
