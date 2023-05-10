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

  public IActionResult Details(int orderId)
  {
    var OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId, includeProperties: "ApplicationUser,DeliveryMethod,DeliveryMethod.PaymentMethod");
    var YerbaMateOrderDetail = _unitOfWork.YerbaMateOrderDetail.GetAll(o => o.OrderId == orderId, includeProperties: "Product").ToList();
    var BombillaOrderDetail = _unitOfWork.BombillaOrderDetail.GetAll(o => o.OrderId == orderId, includeProperties: "Product").ToList();
    var CupOrderDetail = _unitOfWork.CupOrderDetail.GetAll(o => o.OrderId == orderId, includeProperties: "Product").ToList();

    OrderViewModel OrderVM = new(YerbaMateOrderDetail,BombillaOrderDetail,CupOrderDetail,OrderHeader);
    return View(OrderVM);
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
}