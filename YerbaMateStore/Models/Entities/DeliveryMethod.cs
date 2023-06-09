using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using YerbaMateStore.Models.Utilities;

namespace YerbaMateStore.Models.Entities;

public class DeliveryMethod
{
  public int Id { get; set; }

  [Required, MaxLength(200), StringLength(200, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Carrier { get; set; }

  [Required, Display(Name = "Delivery Time"), StringLength(20, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string DeliveryTime { get; set; }

  [Display(Name = "Payment Method")]
  public int PaymentMethodId { get; set; }
  [ForeignKey("PaymentMethodId"), ValidateNever]
  public PaymentMethod PaymentMethod { get; set; }

  [Required, Range(0.00, 100000)]
  public double Cost { get; set; }

  [JsonIgnore]
  public List<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();
}