using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Areas.Customer.Controllers;
[Area("Customer")]
public class CartController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  public ShoppingCartViewModel ShoppingCartVM { get; set; }

  public CartController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public IActionResult Index()
  {
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);
    ShoppingCartVM = new ShoppingCartViewModel()
    {
      YerbaMateCartList = _unitOfWork.YerbaMateShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product"),
      BombillaCartList = _unitOfWork.BombillaShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product"),
      CupCartList = _unitOfWork.CupShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product")
    };
    return View(ShoppingCartVM);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View("Error!");
  }
}