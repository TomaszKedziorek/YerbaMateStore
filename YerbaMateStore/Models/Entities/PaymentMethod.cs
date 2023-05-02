using System.ComponentModel.DataAnnotations;

namespace YerbaMateStore.Models.Entities;

public class PaymentMethod
{
  public int Id { get; set; }
  [Required, StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Name { get; set; }
  public List<DeliveryMethod> DeliveryMethod { get; set; } = new List<DeliveryMethod>();
}

public enum PaymentMethodType
{
  BankTransfer,
  CashOnDelivery
}
