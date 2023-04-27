using System.Security.Claims;

namespace YerbaMateStore.Models.Utilities;

public class UserClaims
{
  public static string GetUserClaimsValue(ClaimsPrincipal user)
  {
    ClaimsIdentity? claimsIdentity = (ClaimsIdentity)user.Identity;
    Claim? claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
    return claim.Value;
  }
}
