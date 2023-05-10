using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
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
    var OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId, includeProperties: "ApplicationUser,DeliveryMethod,DeliveryMethod.PaymentMethod");
    var YerbaMateOrderDetail = _unitOfWork.YerbaMateOrderDetail.GetAll(o => o.OrderId == orderId, includeProperties: "Product").ToList();
    var BombillaOrderDetail = _unitOfWork.BombillaOrderDetail.GetAll(o => o.OrderId == orderId, includeProperties: "Product").ToList();
    var CupOrderDetail = _unitOfWork.CupOrderDetail.GetAll(o => o.OrderId == orderId, includeProperties: "Product").ToList();

    OrderViewModel OrderVM = new(YerbaMateOrderDetail, BombillaOrderDetail, CupOrderDetail, OrderHeader);
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

      var OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == OrderId, includeProperties: "ApplicationUser,DeliveryMethod,DeliveryMethod.PaymentMethod");
      var YerbaMateOrderDetail = _unitOfWork.YerbaMateOrderDetail.GetAll(o => o.OrderId == OrderId, includeProperties: "Product").ToList();
      var BombillaOrderDetail = _unitOfWork.BombillaOrderDetail.GetAll(o => o.OrderId == OrderId, includeProperties: "Product").ToList();
      var CupOrderDetail = _unitOfWork.CupOrderDetail.GetAll(o => o.OrderId == OrderId, includeProperties: "Product").ToList();

      OrderVM = new(YerbaMateOrderDetail, BombillaOrderDetail, CupOrderDetail, OrderHeader);

      return View("Details", OrderVM);
    }
  }

}