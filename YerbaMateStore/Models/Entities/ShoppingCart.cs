using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public class ShoppingCart
{
  public int Id { get; set; }

  [Range(1, 1000, ErrorMessage = "Pleas enter a value between 1 and 1000.")]
  public int Quantity { get; set; }
  public string ApplicationUserId { get; set; }
  [ForeignKey("ApplicationUserId")]
  [ValidateNever]
  public ApplicationUser ApplicationUser { get; set; }
}
