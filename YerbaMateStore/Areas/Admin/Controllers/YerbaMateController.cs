using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
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
    ProductViewModel<YerbaMate> productVM = new ProductViewModel<YerbaMate>(_unitOfWork, null, id);
    return View(productVM);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Upsert(ProductViewModel<YerbaMate> productVM, List<IFormFile> files)
  {
    if (productVM.Product.Id == 0 && (files.Count == 0 || files == null))
    {
      ModelState.AddModelError("Product.Images", "At least one picture is required.");
    }
    if (ModelState.IsValid)
    {
      if (productVM.Product.Id == 0)
      {
        _unitOfWork.YerbaMate.Add(productVM.Product);
      }
      else
      {
        _unitOfWork.YerbaMate.Update(productVM.Product);
      }
      _unitOfWork.Save();

      if (files != null)
      {
        ProductManager<YerbaMate> productManager = new(_hostEnvironment, productVM);
        if (productVM.Product.Images == null)
        {
          productVM.Product.Images = new List<YerbaMateImage>();
        }
        foreach (IFormFile file in files)
        {
          Image<YerbaMate> productImage = new YerbaMateImage();
          productManager.SaveFile(ref productImage, file);
          productVM.Product.Images.Add((YerbaMateImage)productImage);
        }
        _unitOfWork.YerbaMate.Update(productVM.Product);
        _unitOfWork.Save();
      }
      TempData["success"] = "Product updated successfully!";
      return RedirectToAction(nameof(Index));
    }
    else
    {
      productVM = new ProductViewModel<YerbaMate>(_unitOfWork, productVM.Product);
      TempData["error"] = "Product updated failed!";
      return View(productVM);
    }
  }

  [HttpGet]
  public IActionResult DeleteImage(int imageId)
  {
    var imageToDelete = _unitOfWork.YerbaMateImage.GetFirstOrDefault(i => i.Id == imageId);
    int productId = imageToDelete.ProductId;
    var numberOfImages = _unitOfWork.YerbaMateImage.GetAll(i => i.ProductId == productId).Count();
    if (imageToDelete != null && numberOfImages > 1)
    {
      ProductManager<YerbaMate> productManager = new(_hostEnvironment);
      productManager.DeleteFile(imageToDelete);
      _unitOfWork.YerbaMateImage.Remove(imageToDelete);
      _unitOfWork.Save();
      TempData["success"] = "Deleted succesfully!";
    }
    else
    {
      TempData["info"] = "Add a new image before deleting the last one!";
    }
    return RedirectToAction(nameof(Upsert), new { id = productId });
  }


  //API CALLS
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
    else
    {
      ProductManager<YerbaMate> productManager = new(_hostEnvironment);
      productManager.DeleteFiles(id);
      _unitOfWork.YerbaMate.Remove(product);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete Successful!" });
    }
  }
}
