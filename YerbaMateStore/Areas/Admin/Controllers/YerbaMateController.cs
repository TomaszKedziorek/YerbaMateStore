using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Repository.IRepository;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
public class YerbaMateController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public YerbaMateController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }
}
