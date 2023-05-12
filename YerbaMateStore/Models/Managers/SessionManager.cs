using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Models.Managers;

public class SessionManager
{
  private readonly IUnitOfWork _unitOfWork;

  public SessionManager(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public void SetCartSessionValues(ISession httpContextSession, string userClaimsValue)
  {
    var shoppingaCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userClaimsValue);
    int SessionCartCount = shoppingaCart.Sum(u => u.Quantity);
    string SessionCartTotal = Math.Round(shoppingaCart.Sum(u => u.Price * u.Quantity), 2).ToString();
    if (httpContextSession != null)
    {
      httpContextSession.SetInt32(StaticDetails.SessionCartCount, SessionCartCount);
      httpContextSession.SetString(StaticDetails.SessionCartTotal, SessionCartTotal);
    }
  }

  public void CleanSession(ISession httpContextSession)
  {
    if (httpContextSession != null)
    {
      httpContextSession.Clear();
    }
  }

  public CartButtonViewModel GetSessionValues(ISession httpContextSession)
  {
    int? SessionCartCount = httpContextSession.GetInt32(StaticDetails.SessionCartCount);
    string? SessionCartTotal = httpContextSession.GetString(StaticDetails.SessionCartTotal);
    CartButtonViewModel CartButtonVM;
    if (SessionCartCount != null && SessionCartTotal != null)
    {
      CartButtonVM = new((int)SessionCartCount, SessionCartTotal);
    }
    else
    {
      CartButtonVM = new(0, 0);
    }
    return CartButtonVM;
  }
}
