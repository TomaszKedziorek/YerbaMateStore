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
      CupCartList = _unitOfWork.CupShoppingCart.GetAll(c => c.ApplicationUserId == userClaimsValue, "Product"),
    };
    foreach (var item in ShoppingCartVM.YerbaMateCartList)
    {
      item.Price = (double)(item.Product.DiscountPrice == null ? item.Product.Price : item.Product.DiscountPrice);
      item.ImageUrl = _unitOfWork.YerbaMateImage.GetFirstOrDefault(i => i.ProductId == item.ProductId).ImageUrl;
    }
    foreach (var item in ShoppingCartVM.BombillaCartList)
    {
      item.Price = (double)(item.Product.DiscountPrice == null ? item.Product.Price : item.Product.DiscountPrice);
      item.ImageUrl = _unitOfWork.BombillaImage.GetFirstOrDefault(i => i.ProductId == item.ProductId).ImageUrl;
    }
    foreach (var item in ShoppingCartVM.CupCartList)
    {
      item.Price = (double)(item.Product.DiscountPrice == null ? item.Product.Price : item.Product.DiscountPrice);
      item.ImageUrl = _unitOfWork.CupImage.GetFirstOrDefault(i => i.ProductId == item.ProductId).ImageUrl;
    }
    return View(ShoppingCartVM);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View("Error!");
  }
}