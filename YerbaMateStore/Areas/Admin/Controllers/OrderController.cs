using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class OrderController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public OrderController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public IActionResult Index()
  {
    return View();
  }

  [HttpGet]
  public IActionResult GetAll(string status)
  {
    IEnumerable<OrderHeader> orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser,DeliveryMethod,DeliveryMethod.PaymentMethod");
    switch (status)
    {
      case "pending":
        orderHeaderList = orderHeaderList.Where(u => u.PaymentStatus == StaticDetails.PaymentStatusPending);
        break;
      case "inprocess":
        orderHeaderList = orderHeaderList.Where(u => u.OrderStatus == StaticDetails.StatusInProcess);
        break;
      case "completed":
        orderHeaderList = orderHeaderList.Where(u => u.OrderStatus == StaticDetails.StatusShipped);
        break;
      case "approved":
        orderHeaderList = orderHeaderList.Where(u => u.OrderStatus == StaticDetails.StatusApproved);
        break;
      default:
        break;
    }
    return Json(new { data = orderHeaderList });
  }
  public IActionResult Details(int orderId)
  {
    OrderViewModel OrderVM = OrderVM = CreateOrderVM(orderId);
    return View(OrderVM);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult UpdateOrderDetail(OrderViewModel OrderVM)
  {
    int OrderId = OrderVM.OrderHeader.Id;
    if (ModelState.IsValid)
    {
      OrderHeader orderHeaderFromDb = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == OrderId, includeProperties: "ApplicationUser", false);
      orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
      orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
      orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
      orderHeaderFromDb.City = OrderVM.OrderHeader.City;
      orderHeaderFromDb.State = OrderVM.OrderHeader.State;
      orderHeaderFromDb.Country = OrderVM.OrderHeader.Country;
      orderHeaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;
      if (OrderVM.OrderHeader.Carrier != null && orderHeaderFromDb.Carrier != OrderVM.OrderHeader.Carrier)
      {
        orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
      }
      if (OrderVM.OrderHeader.TrackingNumber != null)
      {
        orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
      }
      _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
      _unitOfWork.Save();
      TempData["success"] = "Order details updated successfully!";
      return RedirectToAction("Details", "Order", new { orderId = OrderId });
    }
    else
    {
      TempData["error"] = "Order details update failed!";
      OrderVM = CreateOrderVM(OrderId);
      return View("Details", OrderVM);
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
  public IActionResult StartProcessing(OrderViewModel OrderVM)
  {
    int OrderId = OrderVM.OrderHeader.Id;
    if (ModelState.IsValid)
    {
      _unitOfWork.OrderHeader.UpdateStatus(OrderId, StaticDetails.StatusInProcess);
      _unitOfWork.Save();
      TempData["Success"] = "Order Status updated successfully.";
      return RedirectToAction("Details", "Order", new { orderId = OrderId });
    }
    else
    {
      TempData["error"] = "Order Status update failed.";
      OrderVM = CreateOrderVM(OrderId);
      return View("Details", OrderVM);
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
  public IActionResult ShipOrder(OrderViewModel OrderVM)
  {
    int OrderId = OrderVM.OrderHeader.Id;
    OrderHeader orderHeaderFromDb = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == OrderId, includeProperties: "ApplicationUser", false);
    if (string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
    {
      ModelState.AddModelError("OrderHeader.Carrier", "Carrier field is required!");
    }
    if (string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
    {
      ModelState.AddModelError("OrderHeader.TrackingNumber", "Tracking Number field is required!");
    }
    if (ModelState.IsValid)
    {
      orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
      orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
      orderHeaderFromDb.OrderStatus = StaticDetails.StatusShipped;
      orderHeaderFromDb.ShippingDate = DateTime.Now;
      _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
      _unitOfWork.Save();
      TempData["success"] = "Order Status updated successfully.";
      return RedirectToAction("Details", "Order", new { orderId = OrderVM.OrderHeader.Id });
    }
    else
    {
      TempData["error"] = "Order Status update failed.";
      OrderVM = CreateOrderVM(OrderId);
      return View("Details", OrderVM);
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
  public IActionResult CancelOrder(OrderViewModel OrderVM)
  {
    int OrderId = OrderVM.OrderHeader.Id;
    OrderHeader orderHeaderFromDb = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == OrderId, includeProperties: "ApplicationUser", false);
    if (ModelState.IsValid)
    {
      if (orderHeaderFromDb.PaymentStatus == StaticDetails.PaymentStatusApproved)
      {
        RefundCreateOptions options = new RefundCreateOptions
        {
          Reason = RefundReasons.RequestedByCustomer,
          PaymentIntent = orderHeaderFromDb.PaymentIntentId,
        };
        RefundService service = new RefundService();
        Refund refund = service.Create(options);

        _unitOfWork.OrderHeader.UpdateStatus(orderHeaderFromDb.Id, StaticDetails.StatusCancelled, StaticDetails.StatusRefunded);
      }
      else
      {
        _unitOfWork.OrderHeader.UpdateStatus(OrderId, StaticDetails.StatusCancelled, StaticDetails.StatusCancelled);
      }
      _unitOfWork.Save();
      TempData["Success"] = "Order Cancelled successfully.";
      return RedirectToAction("Details", "Order", new { orderId = OrderId });
    }
    else
    {
      TempData["error"] = "Order Cancelled failed.";
      OrderVM = CreateOrderVM(OrderId);
      return View("Details", OrderVM);
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult PayNow(OrderViewModel OrderVM)
  {
    int OrderId = OrderVM.OrderHeader.Id;
    OrderVM = CreateOrderVM(OrderId);
    // string? OrderApplicationUserId = OrderVM.OrderHeader.ApplicationUserId;
    // string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    if (ModelState.IsValid)
    {
      StripePaymentManager<OrderDetail> StripeManager = new(
        OrderVM.OrderHeader,
        OrderVM.YerbaMateOrderDetailList,
        OrderVM.BombillaOrderDetailList,
        OrderVM.CupOrderDetailList);
      Session session = StripeManager.StripePayment($"Customer/Order/PaymentConfirmation?orderId={OrderId}", $"Admin/Order/Details?orderId={OrderId}");
      _unitOfWork.OrderHeader.UpdateStripePaymentId(OrderId, session.Id, session.PaymentIntentId);
      _unitOfWork.Save();

      Response.Headers.Add("Location", session.Url);
      return new StatusCodeResult(303);
    }
    else
    {
      TempData["error"] = "Something went wrong!";
      return View("Details", OrderVM);
    }
  }

  [HttpGet]
  public IActionResult PaymentConfirmation(int orderId)
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId && o.ApplicationUserId == userClaimsValue, "DeliveryMethod,DeliveryMethod.PaymentMethod");
    if (orderHeader.PaymentType == StaticDetails.PaymentTypeTransfer)
    {
      Session session = StripePaymentManager<OrderDetail>.GetSessionById(orderHeader.SessionId);
      if (session.PaymentStatus.ToLower() == "paid")
      {
        _unitOfWork.OrderHeader.UpdateStripePaymentId(orderId, orderHeader.SessionId, session.PaymentIntentId);
        _unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusApproved, StaticDetails.PaymentStatusApproved);
        _unitOfWork.Save();
        TempData["success"] = "Order has been paid successfully!";
      }
    }
    return View(orderHeader);
  }

  private OrderViewModel CreateOrderVM(int OrderId)
  {
    var OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == OrderId, includeProperties: "ApplicationUser,DeliveryMethod,DeliveryMethod.PaymentMethod");
    var YerbaMateOrderDetail = _unitOfWork.YerbaMateOrderDetail.GetAll(o => o.OrderId == OrderId, includeProperties: "Product").ToList();
    var BombillaOrderDetail = _unitOfWork.BombillaOrderDetail.GetAll(o => o.OrderId == OrderId, includeProperties: "Product").ToList();
    var CupOrderDetail = _unitOfWork.CupOrderDetail.GetAll(o => o.OrderId == OrderId, includeProperties: "Product").ToList();
    OrderViewModel OrderVM = new(YerbaMateOrderDetail, BombillaOrderDetail, CupOrderDetail, OrderHeader);
    return OrderVM;
  }
}