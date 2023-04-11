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
    if (productVM.YerbaMate.Id == 0 && (files.Count == 0 || files == null))
    {
      ModelState.AddModelError("YerbaMate.Images", "At least one picture is required.");
    }
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
          string fileName = Guid.NewGuid().ToString();
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
            ImageUrl = @$"{separation}{productPath}{separation}{fileName}{extension}",
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

  [HttpGet]
  public IActionResult DeleteImage(int imageId)
  {
    var imageToDelete = _unitOfWork.YerbaMateImage.GetFirstOrDefault(i => i.Id == imageId);
    int productId = imageToDelete.ProductId;
    if (imageToDelete != null)
    {
      if (!string.IsNullOrEmpty(imageToDelete.ImageUrl))
      {
        string productImageDbPath = imageToDelete.ImageUrl.Trim('/');
        string imagePath = Path.Combine(_hostEnvironment.WebRootPath, productImageDbPath);
        if (System.IO.File.Exists(imagePath))
        {
          System.IO.File.Delete(imagePath);
        }
      }
      _unitOfWork.YerbaMateImage.Remove(imageToDelete);
      _unitOfWork.Save();
      TempData["success"] = "Deleted succesfully!";
    }
    return RedirectToAction(nameof(Upsert), new { id = productId });
  }


  #region API CALLS
  [HttpGet]
  public IActionResult GetAll()
  {
    IEnumerable<YerbaMate>? productList = _unitOfWork.YerbaMate.GetAll();
    return Json(new { data = productList });
  }

  [HttpDelete]
  public IActionResult Delete(int? id)
  {
    YerbaMate? product = _unitOfWork.YerbaMate.GetFirstOrDefault(p => p.Id == id);

    if (product == null)
    {
      return Json(new { success = false, message = "Error while deleting!" });
    }
    string wwwRootPath = _hostEnvironment.WebRootPath;
    char separation = Path.DirectorySeparatorChar;
    string productPath = @$"images{separation}products{separation}product-{id}";
    string finalPath = Path.Combine(wwwRootPath, productPath);
    if (Directory.Exists(finalPath))
    {
      string[] paths = Directory.GetFiles(finalPath);
      foreach (var path in paths)
      {
        System.IO.File.Delete(path);
      }
      Directory.Delete(finalPath);
    }

    _unitOfWork.YerbaMate.Remove(product);
    _unitOfWork.Save();
    return Json(new { success = true, message = "Delete Successful!" });
  }
  #endregion
}
