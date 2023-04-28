using System.Reflection;
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

  public ShoppingCartViewModel()
  {
  }

  public ShoppingCartViewModel(
    IEnumerable<YerbaMateShoppingCart> yerbaMateCartList,
    IEnumerable<BombillaShoppingCart> bombillaCartList,
    IEnumerable<CupShoppingCart> cupCartList)
  {
    YerbaMateCartList = yerbaMateCartList;
    BombillaCartList = bombillaCartList;
    CupCartList = cupCartList;
  }

  public void SetPrices()
  {
    foreach (var cart in YerbaMateCartList)
    {
      cart.Price = (double)(cart.Product.DiscountPrice == null ? cart.Product.Price : cart.Product.DiscountPrice);
    }
    foreach (var cart in BombillaCartList)
    {
      cart.Price = (double)(cart.Product.DiscountPrice == null ? cart.Product.Price : cart.Product.DiscountPrice);
    }
    foreach (var cart in CupCartList)
    {
      cart.Price = (double)(cart.Product.DiscountPrice == null ? cart.Product.Price : cart.Product.DiscountPrice);
    }
  }
}
