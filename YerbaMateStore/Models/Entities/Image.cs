using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace YerbaMateStore.Models.Entities;

public abstract class Image<T> where T : class
{
  public int Id { get; set; }
  public string ImageUrl { get; set; }
  public int ProductId { get; set; }
  [ValidateNever]
  public T Product { get; set; }
}
