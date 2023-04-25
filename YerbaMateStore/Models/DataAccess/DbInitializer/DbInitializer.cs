using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Utilities;

namespace YerbaMateStore.Models.DataAccess.DbInitializer;

public class DbInitializer : IDbInitializer
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;
  private readonly AppDbContext _dbContext;
  private readonly AdminData _adminData;
  private readonly CountrySeeder _countrySeeder;

  public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
                       AppDbContext dbContext, AdminData adminData)
  {
    _userManager = userManager;
    _roleManager = roleManager;
    _dbContext = dbContext;
    _adminData = adminData;
    _countrySeeder = new(dbContext);
  }

  public void Initialize()
  {
    try
    {
      if (_dbContext.Database.GetPendingMigrations().Count() > 0)
      {
        _dbContext.Database.Migrate();
      }
    }
    catch (Exception e)
    {
    }
    _countrySeeder.Seed();

    if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult())
    {
      _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();
      _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Employee)).GetAwaiter().GetResult();
      _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Individual)).GetAwaiter().GetResult();
    }
    if (_userManager.GetUsersInRoleAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult().Count == 0)
    {
      ApplicationUser admin = new()
      {
        UserName = "admin@test.com",
        Email = _adminData.Email,
        Name = "Thomas Smith",
        PhoneNumber = "678 345 129",
      };
      _userManager.CreateAsync(admin, _adminData.Password).GetAwaiter().GetResult();
      ApplicationUser user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == _adminData.Email);
      _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();
    }
    return;
  }
}
