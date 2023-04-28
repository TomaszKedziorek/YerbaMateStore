using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
public class CupController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IWebHostEnvironment _hostEnvironment;

  public CupController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
    ProductViewModel<Cup> productVM = new ProductViewModel<Cup>(_unitOfWork, null, id);
    return View(productVM);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Upsert(ProductViewModel<Cup> productVM, List<IFormFile> files)
  {
    if (productVM.Product.Id == 0 && (files.Count == 0 || files == null))
    {
      ModelState.AddModelError("Product.Images", "At least one picture is required.");
    }
    if (ModelState.IsValid)
    {
      if (productVM.Product.Id == 0)
      {
        _unitOfWork.Cup.Add(productVM.Product);
      }
      else
      {
        _unitOfWork.Cup.Update(productVM.Product);
      }
      _unitOfWork.Save();

      if (files != null)
      {
        ProductManager<Cup> productManager = new(_hostEnvironment, productVM);
        if (productVM.Product.Images == null)
        {
          productVM.Product.Images = new List<CupImage>();
        }
        foreach (IFormFile file in files)
        {
          Image<Cup> productImage = new CupImage();
          productManager.SaveFile(ref productImage, file);
          productVM.Product.Images.Add((CupImage)productImage);
        }
        _unitOfWork.Cup.Update(productVM.Product);
        _unitOfWork.Save();
      }
      TempData["success"] = "Product updated successfully!";
      return RedirectToAction(nameof(Index));
    }
    else
    {
      productVM = new ProductViewModel<Cup>(_unitOfWork, productVM.Product);
      TempData["error"] = "Product updated failed!";
      return View(productVM);
    }
  }

  [HttpGet]
  public IActionResult DeleteImage(int imageId)
  {
    var imageToDelete = _unitOfWork.CupImage.GetFirstOrDefault(i => i.Id == imageId);
    int productId = imageToDelete.ProductId;
    var numberOfImages = _unitOfWork.CupImage.GetAll(i => i.ProductId == productId).Count();
    if (imageToDelete != null && numberOfImages > 1)
    {
      ProductManager<Cup> productManager = new(_hostEnvironment);
      productManager.DeleteFile(imageToDelete);
      _unitOfWork.CupImage.Remove(imageToDelete);
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
    IEnumerable<Cup>? productList = _unitOfWork.Cup.GetAll();
    return Json(new { data = productList });
  }

  [HttpDelete]
  public IActionResult Delete(int? id)
  {
    Cup? product = _unitOfWork.Cup.GetFirstOrDefault(p => p.Id == id);

    if (product == null)
    {
      return Json(new { success = false, message = "Error while deleting!" });
    }
    else
    {
      ProductManager<Cup> productManager = new(_hostEnvironment);
      productManager.DeleteFiles(id);
      _unitOfWork.Cup.Remove(product);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete Successful!" });
    }
  }
}
