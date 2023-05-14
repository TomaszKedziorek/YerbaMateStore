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
      CartButtonVM = _sessionManager.GetSessionValues(HttpContext.Session);
    }
    else
    {
      double cartTotal = 0;
      CartButtonVM = new(0, cartTotal.ToString("C", StaticDetails.Culture));
    }
    return View(CartButtonVM);
  }
}
