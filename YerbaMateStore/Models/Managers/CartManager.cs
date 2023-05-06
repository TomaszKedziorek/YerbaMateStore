using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Models.Managers;

public class CartManager
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ShoppingCartViewModel _cartVM;
  private IEnumerable<YerbaMateOrderDetail> YerbaMateOrderDetailList { get; set; }
  private IEnumerable<BombillaOrderDetail> BombillaOrderDetailList { get; set; }
  private IEnumerable<CupOrderDetail> CupOrderDetailList { get; set; }

  public CartManager(IUnitOfWork unitOfWork, ShoppingCartViewModel cartVM)
  {
    _unitOfWork = unitOfWork;
    _cartVM = cartVM;
  }

  public void CreateOrderDetails()
  {
    int id = _cartVM.OrderHeader.Id;
    YerbaMateOrderDetailList = CreateOrderDetails<YerbaMateShoppingCart, YerbaMateOrderDetail>(_cartVM.YerbaMateCartList, id);
    BombillaOrderDetailList = CreateOrderDetails<BombillaShoppingCart, BombillaOrderDetail>(_cartVM.BombillaCartList, id);
    CupOrderDetailList = CreateOrderDetails<CupShoppingCart, CupOrderDetail>(_cartVM.CupCartList, id);
  }

  public void AddOrderDetailsToDBThroughRepository()
  {
    _unitOfWork.YerbaMateOrderDetail.AddRange(YerbaMateOrderDetailList);
    _unitOfWork.BombillaOrderDetail.AddRange(BombillaOrderDetailList);
    _unitOfWork.CupOrderDetail.AddRange(CupOrderDetailList);
  }

  public void CleanShoppingCart()
  {
    _unitOfWork.ShoppingCart.RemoveRange( _cartVM.YerbaMateCartList);
    _unitOfWork.ShoppingCart.RemoveRange( _cartVM.BombillaCartList);
    _unitOfWork.ShoppingCart.RemoveRange( _cartVM.CupCartList);
  }

  public IEnumerable<F> CreateOrderDetails<T, F>(IEnumerable<T> cartList, int orderHeaderId) where T : ShoppingCart where F : OrderDetail, new()
  {
    var orderDetailList = new List<F>();
    Parallel.ForEach(cartList, (currentItem) =>
    {
      var cart_ProductId = typeof(T).GetProperty("ProductId").GetValue(currentItem);
      var cart_Price = typeof(T).GetProperty("Price").GetValue(currentItem);
      var cart_Quantity = typeof(T).GetProperty("Quantity").GetValue(currentItem);

      var orderDetail = new F();
      var orderDetail_ProductId = orderDetail.GetType().GetProperty("ProductId");
      orderDetail_ProductId.SetValue(orderDetail, cart_ProductId);
      orderDetail.OrderId = orderHeaderId;
      orderDetail.Price = (double)cart_Price;
      orderDetail.Quantity = (int)cart_Quantity;
      orderDetailList.Add(orderDetail);
    });
    return orderDetailList;
  }
}
