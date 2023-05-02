using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using YerbaMateStore.Models.Utilities;

namespace YerbaMateStore.Models.Entities;

public class DeliveryMethod
{
  public int Id { get; set; }

  [Required, StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Carrier { get; set; }

  [Display(Name = "Collection Point"), StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string CollectionPoint { get; set; }

  [Required, Display(Name = "Delivery time"), StringLength(20, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string DeliveryTime { get; set; }

  public int PaymentMethodId { get; set; }
  [ForeignKey("PaymentMethodId"), ValidateNever]
  public PaymentMethod PaymentMethod { get; set; }

  [Required, Range(0.01, 100000)]
  public double Cost { get; set; }

}