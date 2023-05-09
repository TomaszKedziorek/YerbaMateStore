using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;

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
}