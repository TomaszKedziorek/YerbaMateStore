using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Models.ViewModels;

public class OrderViewModel
{
  public OrderHeader OrderHeader { get; set; }
  [ValidateNever]
  public IEnumerable<YerbaMateOrderDetail> YerbaMateOrderDetailList { get; set; }
  [ValidateNever]
  public IEnumerable<BombillaOrderDetail> BombillaOrderDetailList { get; set; }
  [ValidateNever]
  public IEnumerable<CupOrderDetail> CupOrderDetailList { get; set; }

  public OrderViewModel()
  {
  }

  public OrderViewModel(
    IEnumerable<YerbaMateOrderDetail> yerbaMateOrderDetailList,
    IEnumerable<BombillaOrderDetail> bombillaOrderDetailList,
    IEnumerable<CupOrderDetail> cupOrderDetailList, OrderHeader orderHeader)
  {
    YerbaMateOrderDetailList = yerbaMateOrderDetailList;
    BombillaOrderDetailList = bombillaOrderDetailList;
    CupOrderDetailList = cupOrderDetailList;
    OrderHeader = orderHeader;
  }
}
