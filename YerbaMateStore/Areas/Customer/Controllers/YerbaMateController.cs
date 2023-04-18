using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
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
  public IActionResult Index(string type, string country, string brand, string weight)
  {
    YerbaMateProductsViewModel YerbaMateVM = new(_unitOfWork, type, country, brand, weight);
    return View(YerbaMateVM);
  }
  // [HttpGet]
  // public IActionResult Index(string condition)
  // {
  //   IEnumerable<YerbaMate> productList = GetAllWhere(condition);
  //   if (productList == null)
  //   {
  //     productList = _unitOfWork.YerbaMate.GetAll(includeProperties: "Images");
  //   }
  //   return View(productList);
  // }

  // private IEnumerable<YerbaMate> GetAllWhere(string condition)
  // {
  //   Expression<Func<YerbaMate, bool>> filter;
  //   switch (condition)
  //   {
  //     case "all":
  //       filter = p => p.Id != 0;
  //       break;
  //     case "classic":
  //       filter = p => p.HasAdditives == false;
  //       break;
  //     case "additives":
  //       filter = p => p.HasAdditives == true;
  //       break;
  //     default:
  //       filter = p => p.Id != 0;
  //       break;
  //   }
  //   IEnumerable<YerbaMate> productList = _unitOfWork.YerbaMate.GetAll(filter, includeProperties: "Images");
  //   return productList;
  // }


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
