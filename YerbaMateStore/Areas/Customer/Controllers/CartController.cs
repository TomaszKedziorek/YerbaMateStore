using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
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
      CartVM.OrderHeader.Carrier = CartVM.OrderHeader.DeliveryMethod.Carrier;
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

      OrderManager orderManager = new(_unitOfWork, CartVM);
      orderManager.CreateOrderDetailsForAllProducts();
      orderManager.AddOrderDetailsToDBThroughRepositoryButNotSaveYet();
      _unitOfWork.Save();

      if (CartVM.OrderHeader.DeliveryMethod.PaymentMethod.IsTransfer)
      {
        //Stripe payment
        SessionCreateOptions options = orderManager.StripePayment();
        var service = new SessionService();
        Session session = service.Create(options);
        _unitOfWork.OrderHeader.UpdateStripePaymentId(CartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
        _unitOfWork.Save();

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
      }
      else
      {
        var domain = StaticDetails.Domain;
        var url = domain + $"Customer/Cart/OrderConfirmation?id={CartVM.OrderHeader.Id}";

        return RedirectToAction(nameof(OrderConfirmation), new { CartVM.OrderHeader.Id });
      }
    }
    else
    {
      TempData["error"] = "Something went wrong!";
      return View(CartVM);
    }
  }

  [HttpGet]
  public IActionResult OrderConfirmation(int Id)
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == Id && o.ApplicationUserId == userClaimsValue, "DeliveryMethod,DeliveryMethod.PaymentMethod");
    if (orderHeader != null)
    {
      if (orderHeader.PaymentType == StaticDetails.PaymentTypeTransfer)
      {
        var service = new SessionService();
        Session session = service.Get(orderHeader.SessionId);
        if (session.PaymentStatus.ToLower() == "paid")
        {
          _unitOfWork.OrderHeader.UpdateStripePaymentId(Id, session.Id, session.PaymentIntentId);
          _unitOfWork.OrderHeader.UpdateStatus(Id, StaticDetails.StatusApproved, StaticDetails.PaymentStatusApproved);
          orderHeader.PaymentDate = DateTime.Now;
        }
      }
      else
      {
        _unitOfWork.OrderHeader.UpdateStatus(Id, StaticDetails.StatusApproved, StaticDetails.PaymentStatusOnPickup);
      }
      _unitOfWork.Save();

      var YerbaMateCartList = _unitOfWork.YerbaMateShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
      var BombillaCartList = _unitOfWork.BombillaShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
      var CupCartList = _unitOfWork.CupShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
      CartVM = new ShoppingCartViewModel(YerbaMateCartList, BombillaCartList, CupCartList);

      OrderManager orderManager = new(_unitOfWork, CartVM);
      orderManager.CleanShoppingCartButNotSaveYet();
      _unitOfWork.Save();
      TempData["success"] = "Order has been placed successfully!";

      return View(orderHeader);
    }
    else
    {
      return RedirectToAction("Index", "Home");
    }
  }
}
