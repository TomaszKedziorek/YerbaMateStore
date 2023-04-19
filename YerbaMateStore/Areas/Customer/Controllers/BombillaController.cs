using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class BombillaController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public BombillaController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  [HttpGet]
  public IActionResult Index(string type, string length, string material)
  {
    BombillaProductsViewModel BombillaVM = new(_unitOfWork, type, length, material);
    return View(BombillaVM);
  }

  [HttpGet]
  public IActionResult Details(int productId)
  {
    ShopProductViewModel<Bombilla> productVM = new ShopProductViewModel<Bombilla>(_unitOfWork, productId);
    return View(productVM);
  }

}
