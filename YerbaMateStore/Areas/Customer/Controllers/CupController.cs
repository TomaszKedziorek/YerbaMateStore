using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class CupController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public CupController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  [HttpGet]
  public IActionResult Index(string type, string volume, string material)
  {
    CupProductsViewModel CupVM = new(_unitOfWork,type, volume, material);
    return View(CupVM);
  }

  [HttpGet]
  public IActionResult Details(int productId)
  {
    ShopProductViewModel<Cup> productVM = new ShopProductViewModel<Cup>(_unitOfWork, productId);
    return View(productVM);
  }

}
