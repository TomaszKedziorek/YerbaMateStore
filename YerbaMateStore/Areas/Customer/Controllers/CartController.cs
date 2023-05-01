using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Areas.Customer.Controllers;
[Area("Customer")]
[Authorize]
public class CartController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  public ShoppingCartViewModel ShoppingCartVM { get; set; }

  public CartController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public IActionResult Index()
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    var YerbaMateCartList = _unitOfWork.YerbaMateShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    var BombillaCartList = _unitOfWork.BombillaShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    var CupCartList = _unitOfWork.CupShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");

    ShoppingCartVM = new ShoppingCartViewModel(YerbaMateCartList, BombillaCartList, CupCartList);
    ShoppingCartVM.SetPrices();
    ShoppingCartVM.CalculateTotalPrice();
    if (YerbaMateCartList.Count() == 0 && BombillaCartList.Count() == 0 && CupCartList.Count() == 0)
    {
      ShoppingCartVM.IsCartEmpty = true;
    }
    else
    {
      ShoppingCartVM.IsCartEmpty = false;
    }

    return View(ShoppingCartVM);
  }

  public IActionResult PlusOne(int cartId)
  {
    var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
    _unitOfWork.ShoppingCart.IncrementQuantity(cart, 1);
    _unitOfWork.Save();
    return Redirect(nameof(Index));
  }
  public IActionResult MinusOne(int cartId)
  {
    var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
    if (cart.Quantity <= 1)
    {
      _unitOfWork.ShoppingCart.Remove(cart);
    }
    else
    {
      _unitOfWork.ShoppingCart.DecrementQuantity(cart, 1);
    }
    _unitOfWork.Save();
    return Redirect(nameof(Index));
  }

  public IActionResult Remove(int cartId)
  {
    var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
    _unitOfWork.ShoppingCart.Remove(cart);
    _unitOfWork.Save();
    return Redirect(nameof(Index));
  }

  public IActionResult Summary()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View("Error!");
  }
}