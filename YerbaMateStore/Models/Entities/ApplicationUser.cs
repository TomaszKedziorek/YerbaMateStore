using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace YerbaMateStore.Models.Entities;
public class ApplicationUser : IdentityUser
{
  [Required]
  public string Name { get; set; }
  public string? StreetAddres { get; set; }
  public string? Country { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public string? PostalCode { get; set; }
}
