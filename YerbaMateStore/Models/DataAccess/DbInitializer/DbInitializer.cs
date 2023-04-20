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

  public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
                       AppDbContext appDbContext)
  {
    _userManager = userManager;
    _roleManager = roleManager;
    _appDbContext = appDbContext;
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

      ApplicationUser admin = new()
      {
        UserName = "admin@test.com",
        Email = "admin@test.com",
        Name = "Thomas Smith",
        PhoneNumber = "678 345 129",
        StreetAddress = "Some Street",
        State = "FU",
        PostalCode = "69-777",
        City = "Chicago"
      };
      // _userManager.CreateAsync(admin, _adminData.Password).GetAwaiter().GetResult();
      _userManager.CreateAsync(admin, "Qwerty`1").GetAwaiter().GetResult();
      ApplicationUser user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.Email == admin.Email);
      _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();
    }
    return;
  }
}
