using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Areas.Admin.Controllers;
[Area("Admin")]
public class PaymentMethodController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public PaymentMethodController(IUnitOfWork unitOfWork)
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
    PaymentMethod paymentMethod = _unitOfWork.PaymentMethod.GetFirstOrDefault(m => m.Id == id);
    if (paymentMethod == null)
    {
      paymentMethod = new();
    }
    return View(paymentMethod);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Upsert(PaymentMethod paymentMethod)
  {
    if (ModelState.IsValid)
    {
      if (paymentMethod.Id == 0)
      {
        _unitOfWork.PaymentMethod.Add(paymentMethod);
        TempData["success"] = "Method created successfully!";
      }
      else
      {
        _unitOfWork.PaymentMethod.Update(paymentMethod);
        TempData["success"] = "Method updated successfully!";
      }
      _unitOfWork.Save();
      return RedirectToAction(nameof(Index));
    }
    else
    {
      TempData["error"] = "Method updated failed!";
      return View(paymentMethod);
    }
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    IEnumerable<PaymentMethod>? paymentMethodList = _unitOfWork.PaymentMethod.GetAll();
    return Json(new { data = paymentMethodList });
  }

  [HttpDelete]
  public IActionResult Delete(int? id)
  {
    var paymentMethod = _unitOfWork.PaymentMethod.GetFirstOrDefault(m => m.Id == id);
    if (paymentMethod == null)
    {
      return Json(new { success = false, message = "Error while deleting!" });
    }
    else
    {
      _unitOfWork.PaymentMethod.Remove(paymentMethod);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete Successful!" });
    }
  }
}
