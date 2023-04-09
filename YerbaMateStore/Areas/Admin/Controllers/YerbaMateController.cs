using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
public class YerbaMateController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IWebHostEnvironment _hostEnvironment;

  public YerbaMateController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
  {
    _unitOfWork = unitOfWork;
    _hostEnvironment = hostEnvironment;
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
  public IActionResult Upsert(YerbaMateViewModel productVM, List<IFormFile> files)
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

      string wwwRootPath = _hostEnvironment.WebRootPath;
      if (files != null)
      {
        foreach (IFormFile file in files)
        {
          string extension = Path.GetExtension(file.FileName);
          string fileName = Guid.NewGuid().ToString() + extension;
          char separation = Path.DirectorySeparatorChar;
          string productPath = @$"images{separation}products{separation}product-{productVM.YerbaMate.Id}";
          string finalPath = Path.Combine(wwwRootPath, productPath);
          if (!Directory.Exists(finalPath))
            Directory.CreateDirectory(finalPath);

          using (var fileStream = new FileStream(Path.Combine(finalPath, fileName + extension), FileMode.Create))
          {
            file.CopyTo(fileStream);
          }

          YerbaMateImage productImage = new()
          {
            ProductId = productVM.YerbaMate.Id,
            ImageUrl = @$"{separation}{productPath}{separation}{fileName}.{extension}",
          };
          if (productVM.YerbaMate.Images == null)
            productVM.YerbaMate.Images = new List<YerbaMateImage>();

          productVM.YerbaMate.Images.Add(productImage);
        }
        _unitOfWork.YerbaMate.Update(productVM.YerbaMate);
        _unitOfWork.Save();
      }
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
