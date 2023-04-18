using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Pagination;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class YerbaMateController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public YerbaMateController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  [HttpGet]
  public IActionResult Index(YerbaMateQuery query)
  {
    YerbaMateProductsViewModel YerbaMateVM = new(_unitOfWork, query);
    return View(YerbaMateVM);
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
