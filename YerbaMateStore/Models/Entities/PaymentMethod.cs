using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YerbaMateStore.Models.Entities;

public class PaymentMethod
{
  public int Id { get; set; }
  [Required, StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
  public string Name { get; set; }

  [Required, Display(Name="Is Transfer")]
  public bool IsTransfer { get; set; } 

  [JsonIgnore]
  public List<DeliveryMethod> DeliveryMethod { get; set; } = new List<DeliveryMethod>();
}
