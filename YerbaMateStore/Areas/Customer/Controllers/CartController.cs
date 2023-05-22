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
  private SessionManager _sessionManager;

  public CartController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
    _sessionManager = new(unitOfWork);
  }

  public IActionResult Index()
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    CartVM = CreateCartVM(userClaimsValue);
    return View(CartVM);
  }

  public IActionResult PlusOne(int cartId)
  {
    var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
    _unitOfWork.ShoppingCart.IncrementQuantity(cart, 1);
    _unitOfWork.Save();
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    _sessionManager.SetCartSessionValues(HttpContext.Session, userClaimsValue);
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
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    _sessionManager.SetCartSessionValues(HttpContext.Session, userClaimsValue);
    return Redirect(nameof(Index));
  }

  public IActionResult Remove(int cartId)
  {
    var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
    _unitOfWork.ShoppingCart.Remove(cart);
    _unitOfWork.Save();
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    _sessionManager.SetCartSessionValues(HttpContext.Session, userClaimsValue);
    return Redirect(nameof(Index));
  }

  [HttpGet]
  public IActionResult Summary()
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    CartVM = CreateCartVM(userClaimsValue, addDeliveryMethodList: true);
    if (CartVM.IsCartEmpty)
    {
      return RedirectToAction("Index","Home");
    }
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
    if (CartVM.OrderHeader.DeliveryMethodId == 0)
    {
      ModelState.AddModelError("OrderHeader.DeliveryMethodId", "Select Delivery method.");
    }
    if (ModelState.IsValid)
    {
      var deliveryMethod = _unitOfWork.DeliveryMethod.GetFirstOrDefault(m => m.Id == CartVM.OrderHeader.DeliveryMethodId, "PaymentMethod");
      CartVM = CreateCartVM(userClaimsValue, CartVM, true);
      CartVM.OrderHeader.DeliveryMethod = deliveryMethod;
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

      int OrderId = CartVM.OrderHeader.Id;

      if (CartVM.OrderHeader.DeliveryMethod.PaymentMethod.IsTransfer && StaticDetails.StripePaymentEnabled)
      {
        //Stripe payment
        StripePaymentManager<ShoppingCart> StripeManager = new(
          CartVM.OrderHeader,
          CartVM.YerbaMateCartList,
          CartVM.BombillaCartList,
          CartVM.CupCartList);
        Session session = StripeManager.StripePayment($"Customer/Cart/OrderConfirmation?orderId={OrderId}", $"Customer/Cart/OrderConfirmation?orderId={OrderId}");
        _unitOfWork.OrderHeader.UpdateStripePaymentId(OrderId, session.Id, session.PaymentIntentId);
        _unitOfWork.Save();

        Response.Headers.Add("Location", session.Url);
        orderManager.CleanShoppingCartButNotSaveYet();
        _unitOfWork.Save();
        _sessionManager.CleanSession(HttpContext.Session);
        return new StatusCodeResult(303);
      }
      else if (CartVM.OrderHeader.DeliveryMethod.PaymentMethod.IsTransfer && !StaticDetails.StripePaymentEnabled)
      {
        orderManager.CleanShoppingCartButNotSaveYet();
        _unitOfWork.Save();
        return RedirectToAction(nameof(OnlinePaymentDisabled), new { OrderId });
      }
      else
      {
        orderManager.CleanShoppingCartButNotSaveYet();
        _unitOfWork.Save();
        _sessionManager.CleanSession(HttpContext.Session);
        return RedirectToAction(nameof(OrderConfirmation), new { OrderId });
      }
    }
    else
    {
      CartVM = CreateCartVM(userClaimsValue, CartVM, true);
      TempData["error"] = "Something went wrong!";
      return View(CartVM);
    }
  }

  [HttpGet]
  public IActionResult OrderConfirmation(int orderId)
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId && o.ApplicationUserId == userClaimsValue, "DeliveryMethod,DeliveryMethod.PaymentMethod");
    if (orderHeader != null)
    {
      if (orderHeader.PaymentType == StaticDetails.PaymentTypeTransfer)
      {
        if (StaticDetails.StripePaymentEnabled && orderHeader.PaymentIntentId == null)
        {
          Session session = StripePaymentManager<ShoppingCart>.GetSessionById(orderHeader.SessionId);
          if (session.PaymentStatus.ToLower() == "paid")
          {
            _unitOfWork.OrderHeader.UpdateStripePaymentId(orderId, session.Id, session.PaymentIntentId);
            _unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusApproved, StaticDetails.PaymentStatusApproved);
          }
        }
      }
      else
      {
        _unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusApproved, StaticDetails.PaymentStatusOnPickup);
      }
      _unitOfWork.Save();
      TempData["success"] = "Order has been placed successfully!";

      return View(orderHeader);
    }
    else
    {
      return RedirectToAction("Index", "Home");
    }
  }

  [HttpGet]
  public IActionResult OnlinePaymentDisabled(int orderId)
  {
    if (!StaticDetails.StripePaymentEnabled)
    {
      string userClaimsValue = UserClaims.GetUserClaimsValue(User);
      OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId && o.ApplicationUserId == userClaimsValue, "DeliveryMethod,DeliveryMethod.PaymentMethod");
      OnlinePaymentsDisabledViewModel paymentsDisabledVM = new(orderId, orderHeader.OrderStatus, orderHeader.PaymentStatus);
      return View(paymentsDisabledVM);
    }
    else
    {
      return RedirectToAction("Index", "Home");
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult OnlinePaymentDisabled(OnlinePaymentsDisabledViewModel paymentsDisabledVM)
  {
    if (!StaticDetails.StripePaymentEnabled)
    {
      string userClaimsValue = UserClaims.GetUserClaimsValue(User);
      int orderId = paymentsDisabledVM.OrderId;
      OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId && o.ApplicationUserId == userClaimsValue, "DeliveryMethod,DeliveryMethod.PaymentMethod");
      if (ModelState.IsValid)
      {
        if (paymentsDisabledVM.PaymentStatus == StaticDetails.PaymentStatusApproved)
        {
          string sessionId = Guid.NewGuid().ToString();
          string paymentIntentId = Guid.NewGuid().ToString();
          _unitOfWork.OrderHeader.UpdateStripePaymentId(orderId, sessionId, paymentIntentId);
          _unitOfWork.OrderHeader.UpdateStatus(orderId, paymentsDisabledVM.OrderStatus, paymentsDisabledVM.PaymentStatus);
          _unitOfWork.Save();
          _sessionManager.CleanSession(HttpContext.Session);
        }
        return RedirectToAction(nameof(OrderConfirmation), new { orderId });
      }
      else
      {
        return View(paymentsDisabledVM);
      }
    }
    else
    {
      return RedirectToAction("Index", "Home");
    }
  }


  private ShoppingCartViewModel CreateCartVM(string userClaimsValue, ShoppingCartViewModel CartVM = null, bool addDeliveryMethodList = false)
  {
    var YerbaMateCartList = _unitOfWork.YerbaMateShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    var BombillaCartList = _unitOfWork.BombillaShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    var CupCartList = _unitOfWork.CupShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product,Product.Images");
    if (CartVM == null)
    {
      CartVM = new(YerbaMateCartList, BombillaCartList, CupCartList);
    }
    else
    {
      CartVM.YerbaMateCartList = YerbaMateCartList;
      CartVM.BombillaCartList = BombillaCartList;
      CartVM.CupCartList = CupCartList;
    }
    if (addDeliveryMethodList)
    {
      var deliveryMethodList = _unitOfWork.DeliveryMethod.GetAll(includeProperties: "PaymentMethod")
        .OrderBy(m => m.Carrier)
        .ThenByDescending(m => m.PaymentMethod.IsTransfer);
      CartVM.DeliveryMethodList = deliveryMethodList;
    }
    return CartVM;
  }
}
