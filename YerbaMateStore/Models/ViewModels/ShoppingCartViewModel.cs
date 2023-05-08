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
  [ValidateNever]
  public IEnumerable<DeliveryMethod> DeliveryMethodList { get; set; }
  public OrderHeader OrderHeader { get; set; }
  [ValidateNever]
  public bool IsCartEmpty { get; set; }


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
    OrderHeader = new();
    SetPrices();
    CalculateTotalPrice();
    CheckIfCartIsEmpty();
  }
  public ShoppingCartViewModel(
    IEnumerable<YerbaMateShoppingCart> yerbaMateCartList,
    IEnumerable<BombillaShoppingCart> bombillaCartList,
    IEnumerable<CupShoppingCart> cupCartList,
    IEnumerable<DeliveryMethod> deliveryMethodList)
    : this(yerbaMateCartList, bombillaCartList, cupCartList)
  {
    DeliveryMethodList = deliveryMethodList;
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

  private void CalculateTotalPrice()
  {
    double total = 0;
    foreach (var cart in YerbaMateCartList)
    {
      total += (cart.Quantity * cart.Price);
    }
    foreach (var cart in BombillaCartList)
    {
      total += (cart.Quantity * cart.Price);
    }
    foreach (var cart in CupCartList)
    {
      total += (cart.Quantity * cart.Price);
    }
    this.OrderHeader.OrderTotal = total;
  }

  private void CheckIfCartIsEmpty()
  {
    if (YerbaMateCartList.Count() == 0 && BombillaCartList.Count() == 0 && CupCartList.Count() == 0)
    {
      IsCartEmpty = true;
    }
    else
    {
      IsCartEmpty = false;
    }
  }
}
