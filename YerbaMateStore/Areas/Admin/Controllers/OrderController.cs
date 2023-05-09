using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
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
  public IActionResult GetAll()
  {
    IEnumerable<OrderHeader> orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser,DeliveryMethod,DeliveryMethod.PaymentMethod");
    return Json(new { data = orderHeaderList });
  }
}