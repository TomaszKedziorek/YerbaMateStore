using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_Employee)]
public class BombillaController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IWebHostEnvironment _hostEnvironment;

  public BombillaController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
    ProductViewModel<Bombilla> productVM = new ProductViewModel<Bombilla>(_unitOfWork, null, id);
    return View(productVM);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Upsert(ProductViewModel<Bombilla> productVM, List<IFormFile> files)
  {
    if (productVM.Product.Id == 0 && (files.Count == 0 || files == null))
    {
      ModelState.AddModelError("Product.Images", "At least one picture is required.");
    }
    if (ModelState.IsValid)
    {
      if (productVM.Product.Id == 0)
      {
        _unitOfWork.Bombilla.Add(productVM.Product);
      }
      else
      {
        _unitOfWork.Bombilla.Update(productVM.Product);
      }
      _unitOfWork.Save();

      if (files != null)
      {
        ProductManager<Bombilla> productManager = new(_hostEnvironment, productVM);
        if (productVM.Product.Images == null)
        {
          productVM.Product.Images = new List<BombillaImage>();
        }
        foreach (IFormFile file in files)
        {
          Image<Bombilla> productImage = new BombillaImage();
          productManager.SaveFile(ref productImage, file);
          productVM.Product.Images.Add((BombillaImage)productImage);
        }
        _unitOfWork.Bombilla.Update(productVM.Product);
        _unitOfWork.Save();
      }
      TempData["success"] = "Product updated successfully!";
      return RedirectToAction(nameof(Index));
    }
    else
    {
      productVM = new ProductViewModel<Bombilla>(_unitOfWork, productVM.Product);
      TempData["error"] = "Product updated failed!";
      return View(productVM);
    }
  }

  [HttpGet]
  public IActionResult DeleteImage(int imageId)
  {
    var imageToDelete = _unitOfWork.BombillaImage.GetFirstOrDefault(i => i.Id == imageId);
    int productId = imageToDelete.ProductId;
    var numberOfImages = _unitOfWork.BombillaImage.GetAll(i => i.ProductId == productId).Count();
    if (imageToDelete != null && numberOfImages > 1)
    {
      ProductManager<Bombilla> productManager = new(_hostEnvironment);
      productManager.DeleteFile(imageToDelete);
      _unitOfWork.BombillaImage.Remove(imageToDelete);
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
    IEnumerable<Bombilla>? productList = _unitOfWork.Bombilla.GetAll();
    return Json(new { data = productList });
  }

  [HttpDelete]
  public IActionResult Delete(int? id)
  {
    Bombilla? product = _unitOfWork.Bombilla.GetFirstOrDefault(p => p.Id == id);

    if (product == null)
    {
      return Json(new { success = false, message = "Error while deleting!" });
    }
    else
    {
      ProductManager<Bombilla> productManager = new(_hostEnvironment);
      productManager.DeleteFiles(id);
      _unitOfWork.Bombilla.Remove(product);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete Successful!" });
    }
  }
}
