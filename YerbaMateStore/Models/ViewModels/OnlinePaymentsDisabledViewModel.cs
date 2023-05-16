using System.ComponentModel.DataAnnotations;

namespace YerbaMateStore.Models.ViewModels;

public class OnlinePaymentsDisabledViewModel
{
  [Required]
  public int OrderId { get; set; }
  [Required]
  public string OrderStatus { get; set; }
  [Required]
  public string PaymentStatus { get; set; }

  public OnlinePaymentsDisabledViewModel()
  {
  }

  public OnlinePaymentsDisabledViewModel(int orderId, string orderStatus, string paymentStatus)
  {
    OrderId = orderId;
    OrderStatus = orderStatus;
    PaymentStatus = paymentStatus;
  }

}
