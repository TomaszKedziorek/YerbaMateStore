using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
public class YerbaMateController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public YerbaMateController(IUnitOfWork unitOfWork)
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
    YerbaMateViewModel productVM = new YerbaMateViewModel(_unitOfWork, null, id);
    return View(productVM);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Upsert(YerbaMateViewModel productVM)
  {
    if (ModelState.IsValid)
    {
      if (productVM.YerbaMate.Id == 0)
      {
        _unitOfWork.YerbaMate.Add(productVM.YerbaMate);
      }
      else
      {
        _unitOfWork.YerbaMate.Update(productVM.YerbaMate);
      }
      _unitOfWork.Save();
      TempData["success"] = "Product updated successfully!";
      return RedirectToAction(nameof(Index));
    }
    else
    {
      productVM = new YerbaMateViewModel(_unitOfWork, productVM.YerbaMate);
      TempData["error"] = "Product updated failed!";
      return View(productVM);
    }
  }

  #region API CALLS
  [HttpGet]
  public IActionResult GetAll()
  {
    IEnumerable<YerbaMate>? productList = _unitOfWork.YerbaMate.GetAll();
    return Json(new { data = productList });
  }

  #endregion
}
