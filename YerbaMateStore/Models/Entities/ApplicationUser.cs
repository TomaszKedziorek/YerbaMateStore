using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace YerbaMateStore.Models.Entities;
public class ApplicationUser : IdentityUser
{
  [Required]
  [PersonalData]
  public string Name { get; set; }
  [PersonalData]
  public string? StreetAddress { get; set; }
  [PersonalData]
  public string? Country { get; set; }
  [PersonalData]
  public string? City { get; set; }
  [PersonalData]
  public string? State { get; set; }
  [PersonalData]
  public string? PostalCode { get; set; }
}
