using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = StaticDetails.Role_Admin)]
public class UsersController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IWebHostEnvironment _hostEnvironment;

  public UsersController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
  {
    _unitOfWork = unitOfWork;
    _hostEnvironment = hostEnvironment;
  }

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

  // [HttpGet]
  // public IActionResult Upsert(int? id)
  // {
  //   ProductViewModel<YerbaMate> productVM = new ProductViewModel<YerbaMate>(_unitOfWork, null, id);
  //   return View(productVM);
  // }

  // //API CALLS
  // [HttpGet]
  // public IActionResult GetAll()
  // {
  //   IEnumerable<YerbaMate>? productList = _unitOfWork.YerbaMate.GetAll();
  //   return Json(new { data = productList });
  // }

  // [HttpDelete]
  // public IActionResult Delete(int? id)
  // {
  //   YerbaMate? product = _unitOfWork.YerbaMate.GetFirstOrDefault(p => p.Id == id);

  //   if (product == null)
  //   {
  //     return Json(new { success = false, message = "Error while deleting!" });
  //   }
  //   else
  //   {
  //     ProductManager<YerbaMate> productManager = new(_hostEnvironment);
  //     productManager.DeleteFiles(id);
  //     _unitOfWork.YerbaMate.Remove(product);
  //     _unitOfWork.Save();
  //     return Json(new { success = true, message = "Delete Successful!" });
  //   }
  // }
}
