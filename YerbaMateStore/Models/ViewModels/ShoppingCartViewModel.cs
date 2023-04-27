using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.ViewModels;
public class ShoppingCartViewModel
{
  [ValidateNever]
  public IEnumerable<YerbaMateShoppingCart> YerbaMateCartList { get; set; }
  [ValidateNever]
  public IEnumerable<BombillaShoppingCart> BombillaCartList { get; set; }
  [ValidateNever]
  public IEnumerable<CupShoppingCart> CupCartList { get; set; }
}
