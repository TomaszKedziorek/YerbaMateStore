using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using System.Security.Claims;
using YerbaMateStore.Models.ViewModels;
using YerbaMateStore.Models.Managers;

namespace YerbaMateStore.ViewComponents;
public class ShoppingCartViewComponent : ViewComponent
{
  private readonly IUnitOfWork _unitOfWork;
  private SessionManager _sessionManager;

  public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
    _sessionManager = new(unitOfWork);
  }

  public async Task<IViewComponentResult> InvokeAsync()
  {
    string? userClaimsValue = UserClaims.GetUserClaimsValue((ClaimsPrincipal)User);
    CartButtonViewModel CartButtonVM;

    if (userClaimsValue != null)
    {
      int? SessionCartCount = HttpContext.Session.GetInt32(StaticDetails.SessionCartCount);
      string? SessionCartTotal = HttpContext.Session.GetString(StaticDetails.SessionCartTotal);
      if (SessionCartCount != null && SessionCartTotal != null)
      {
        CartButtonVM = new((int)SessionCartCount, SessionCartTotal);
      }
      else
      {
        _sessionManager.SetCartSessionValues(HttpContext.Session, userClaimsValue);
        SessionCartCount = HttpContext.Session.GetInt32(StaticDetails.SessionCartCount);
        SessionCartTotal = HttpContext.Session.GetString(StaticDetails.SessionCartTotal);
        CartButtonVM = new((int)SessionCartCount, SessionCartTotal);
      }
    }
    else
    {
      HttpContext.Session.Clear();
      CartButtonVM = new(0, 0);
    }
    return View(CartButtonVM);
  }

  // public void SetCartSessionValues(string userClaimsValue)
  // {
  //   var shoppingaCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userClaimsValue);
  //   int SessionCartCount = shoppingaCart.Sum(u => u.Quantity);
  //   string SessionCartTotal = Math.Round(shoppingaCart.Sum(u => u.Price * u.Quantity), 2).ToString();
  //   HttpContext.Session.SetInt32(StaticDetails.SessionCartCount, SessionCartCount);
  //   HttpContext.Session.SetString(StaticDetails.SessionCartTotal, SessionCartTotal);
  // }
}
