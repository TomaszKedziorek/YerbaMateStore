using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
public class DeliveryMethodController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public DeliveryMethodController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

  [HttpGet]
  public IActionResult Upsert(int? id)
  {
    DeliveryMethod deliveryMethod = _unitOfWork.DeliveryMethod.GetFirstOrDefault(m => m.Id == id, "PaymentMethod");
    if (deliveryMethod == null)
    {
      deliveryMethod = new();
      ViewData["PaymentMethodId"] = new SelectList(_unitOfWork.PaymentMethod.GetAll(), "Id", "Name");
    }
    else
    {
      ViewData["PaymentMethodId"] = new SelectList(_unitOfWork.PaymentMethod.GetAll(), "Id", "Name", deliveryMethod.PaymentMethodId);
    }
    return View(deliveryMethod);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Upsert(DeliveryMethod deliveryMethod)
  {
    if (ModelState.IsValid)
    {
      if (deliveryMethod.Id == 0)
      {
        _unitOfWork.DeliveryMethod.Add(deliveryMethod);
        TempData["success"] = "Product created successfully!";
      }
      else
      {
        _unitOfWork.DeliveryMethod.Update(deliveryMethod);
        TempData["success"] = "Product updated successfully!";
      }
      _unitOfWork.Save();
      return RedirectToAction(nameof(Index));
    }
    else
    {
      ViewData["PaymentMethodId"] = new SelectList(_unitOfWork.PaymentMethod.GetAll(), "Id", "Name", deliveryMethod.PaymentMethodId);
      TempData["error"] = "Product updated failed!";
      return View(deliveryMethod);
    }
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    IEnumerable<DeliveryMethod>? deliveryMethodList = _unitOfWork.DeliveryMethod.GetAll(includeProperties: "PaymentMethod");
    return Json(new { data = deliveryMethodList });
  }

  [HttpDelete]
  public IActionResult Delete(int? id)
  {
    var deliveryMethod = _unitOfWork.DeliveryMethod.GetFirstOrDefault(m => m.Id == id);
    if (deliveryMethod == null)
    {
      return Json(new { success = false, message = "Error while deleting!" });
    }
    else
    {
      _unitOfWork.DeliveryMethod.Remove(deliveryMethod);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete Successful!" });
    }
  }
}

