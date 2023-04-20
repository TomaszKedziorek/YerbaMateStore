using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Utilities;

namespace YerbaMateStore.Models.DataAccess.DbInitializer;

public class DbInitializer : IDbInitializer
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;
  private readonly AppDbContext _appDbContext;
  private readonly AdminData _adminData;

  public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
                       AppDbContext appDbContext, AdminData adminData)
  {
    _userManager = userManager;
    _roleManager = roleManager;
    _appDbContext = appDbContext;
    _adminData = adminData;
  }

  public void Initialize()
  {
    try
    {
      if (_appDbContext.Database.GetPendingMigrations().Count() > 0)
      {
        _appDbContext.Database.Migrate();
      }
    }
    catch (Exception e)
    {
    }
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
      ApplicationUser user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.Email == _adminData.Email);
      _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();
    }
    return;
  }
}
