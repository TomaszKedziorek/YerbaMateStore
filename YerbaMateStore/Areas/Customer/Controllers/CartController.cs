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
  public ShoppingCartViewModel CartVM { get; set; }

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

    CartVM = new ShoppingCartViewModel(YerbaMateCartList, BombillaCartList, CupCartList);

    return View(CartVM);
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

  [HttpGet]
  public IActionResult Summary()
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    var YerbaMateCartList = _unitOfWork.YerbaMateShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    var BombillaCartList = _unitOfWork.BombillaShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    var CupCartList = _unitOfWork.CupShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    var deliveryMethodList = _unitOfWork.DeliveryMethod
      .GetAll(includeProperties: "PaymentMethod")
      .OrderBy(m => m.Carrier)
      .ThenByDescending(m => m.PaymentMethod.IsTransfer);

    CartVM = new ShoppingCartViewModel(YerbaMateCartList, BombillaCartList, CupCartList, deliveryMethodList);
    CartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userClaimsValue);
    CartVM.OrderHeader.Name = CartVM.OrderHeader.ApplicationUser.Name;
    CartVM.OrderHeader.PhoneNumber = CartVM.OrderHeader.ApplicationUser.PhoneNumber;
    CartVM.OrderHeader.StreetAddress = CartVM.OrderHeader.ApplicationUser.StreetAddress;
    CartVM.OrderHeader.Country = CartVM.OrderHeader.ApplicationUser.Country;
    CartVM.OrderHeader.City = CartVM.OrderHeader.ApplicationUser.City;
    CartVM.OrderHeader.State = CartVM.OrderHeader.ApplicationUser.State;
    CartVM.OrderHeader.PostalCode = CartVM.OrderHeader.ApplicationUser.PostalCode;

    return View(CartVM);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Summary(ShoppingCartViewModel CartVM)
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    CartVM.YerbaMateCartList = _unitOfWork.YerbaMateShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    CartVM.BombillaCartList = _unitOfWork.BombillaShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    CartVM.CupCartList = _unitOfWork.CupShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    CartVM.OrderHeader.DeliveryMethod = _unitOfWork.DeliveryMethod.GetFirstOrDefault(m => m.Id == CartVM.OrderHeader.DeliveryMethodId, "PaymentMethod");

    CartVM.SetPrices();

    if (ModelState.IsValid)
    {
      CartVM.OrderHeader.ApplicationUserId = userClaimsValue;
      CartVM.OrderHeader.OrderDate = DateTime.Now;
      if (CartVM.OrderHeader.DeliveryMethod.PaymentMethod.IsTransfer)
      {
        CartVM.OrderHeader.PaymentType = StaticDetails.PaymentTypeTransfer;
      }
      else
      {
        CartVM.OrderHeader.PaymentType = StaticDetails.PaymentTypeOnPickup;
      }
      CartVM.OrderHeader.OrderStatus = StaticDetails.StatusPending;
      CartVM.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusPending;
      double? orderDeliveryTotal = CartVM.OrderHeader.OrderAndDeliveryTotal;
      double orderDeliveryTotalCalculated = CartVM.OrderHeader.OrderTotal + CartVM.OrderHeader.DeliveryMethod.Cost;
      if (orderDeliveryTotal == 0 || orderDeliveryTotal != orderDeliveryTotalCalculated || orderDeliveryTotal == null)
      {
        CartVM.OrderHeader.OrderAndDeliveryTotal = orderDeliveryTotalCalculated;
      }
      _unitOfWork.OrderHeader.Add(CartVM.OrderHeader);
      _unitOfWork.Save();

      CartManager cartManager = new(_unitOfWork, CartVM);
      cartManager.CreateOrderDetails();
      cartManager.AddOrderDetailsToDBThroughRepository();
      cartManager.CleanShoppingCart();

      _unitOfWork.Save();

      TempData["success"] = "Order has been placed!";
      return RedirectToAction("Index", "Home");
    }
    else
    {
      TempData["error"] = "Something went wrong!";
      return View(CartVM);
    }
  }
}
