using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class OrderHeader
{
  public int Id { get; set; }
  [ForeignKey("ApplicationUserId"), ValidateNever]
  public ApplicationUser ApplicationUser { get; set; }
  public string ApplicationUserId { get; set; }

  [Required]
  public DateTime OrderDate { get; set; }
  public DateTime ShippingDate { get; set; }
  public double OrderTotal { get; set; }
  public double OrderAndDeliveryTotal { get; set; }
  public string? OrderStatus { get; set; }
  public string? PaymentStatus { get; set; }
  public string? PaymentType{ get; set; }
  public string? TrackingNumber { get; set; }
  public string? Carrier { get; set; }
  public DateTime PaymentDate { get; set; }

  public string? SessionId { get; set; }
  public string? PaymentIntentId { get; set; }

  [Required, Display(Name = "Delivery Method"), ForeignKey("DeliveryMethodId")]
  public int DeliveryMethodId { get; set; }
  [ValidateNever]
  public DeliveryMethod DeliveryMethod { get; set; }

  [Required, Display(Name = "Phone Number")]
  public string PhoneNumber { get; set; }
  [Required, Display(Name = "Street Address")]
  public string StreetAddress { get; set; }
  [Required]
  public string Country { get; set; }
  [Required]
  public string City { get; set; }
  [Required]
  public string State { get; set; }
  [Required, Display(Name = "Postal Code")]
  public string PostalCode { get; set; }
  [Required]
  public string Name { get; set; }
}