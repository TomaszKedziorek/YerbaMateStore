using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class CupController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public CupController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
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
    ShoppingCartManager<Cup, CupShoppingCart> manager = new(_unitOfWork);
    CupShoppingCart shoppingCart = manager.CreateShoppingCart(productId);
    return View(shoppingCart);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Details(CupShoppingCart shoppingCart)
  {
    if (ModelState.IsValid)
    {
      ClaimsIdentity? claimsIdentity = (ClaimsIdentity)User.Identity;
      Claim? claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
      shoppingCart.ApplicationUserId = claim.Value;

      var shoppingCartFromDb = _unitOfWork.CupShoppingCart.GetFirstOrDefault(i => i.ApplicationUserId == claim.Value && i.ProductId == shoppingCart.ProductId);
      if (shoppingCartFromDb == null)
      {
        _unitOfWork.CupShoppingCart.Add(shoppingCart);
      }
      else
      {
        _unitOfWork.CupShoppingCart.IncrementQuantity(shoppingCartFromDb, shoppingCart.Quantity);
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
