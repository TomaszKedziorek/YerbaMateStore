using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;

namespace YerbaMateStore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class UsersManagerController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly UserManager<IdentityUser> _userManager;
  public UsersManagerController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
  {
    _unitOfWork = unitOfWork;
    _userManager = userManager;
  }

  public IActionResult Index()
  {
    return View();
  }

  [HttpGet]
  public IActionResult GetAll(string status)
  {
    // IEnumerable<ApplicationUser> applicationUserList;
    string userClaimsValue = UserClaims.GetUserClaimsValue(User);

    var applicationUserList = _unitOfWork.ApplicationUser
        .GetAll(u => u.Id != userClaimsValue)
        .Select(u => new
        {
          Id = u.Id,
          Role = _userManager.GetRolesAsync(u).GetAwaiter().GetResult().First(),
          Name = u.Name,
          Email = u.Email,
          EmailConfirmed = u.EmailConfirmed,
          AccountSuspended = (u.LockoutEnd == null || u.LockoutEnd < DateTime.Now) ? false : true,
          LockoutEnd = (u.LockoutEnd == null || u.LockoutEnd < DateTime.Now) ? null : u.LockoutEnd.Value.DateTime.ToString()
        });

    switch (status)
    {
      case "admins":
        applicationUserList = applicationUserList.Where(u => u.Role == StaticDetails.Role_Admin);
        break;
      case "employees":
        applicationUserList = applicationUserList.Where(u => u.Role == StaticDetails.Role_Employee);
        break;
      case "individuals":
        applicationUserList = applicationUserList.Where(u => u.Role == StaticDetails.Role_Individual);
        break;
      case "suspended":
        applicationUserList = applicationUserList.Where(u => u.AccountSuspended == true);
        break;
      case "inactive":
        applicationUserList = applicationUserList.Where(u => u.EmailConfirmed == false);
        break;
      case "all":
        break;
      default:
        break;
    }
    return Json(new { data = applicationUserList });
  }


  [HttpPost]
  public IActionResult ConfirmUserEmail(string userId, bool confirm)
  {
    var applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);
    if (applicationUser != null)
    {
      applicationUser.EmailConfirmed = confirm;
      _unitOfWork.ApplicationUser.Update(applicationUser);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Email confirmed successfully!" });
    }
    else
    {
      return Json(new { success = false, message = "User confirmed unsuccessfully!" });
    }
  }

  [HttpPost]
  public IActionResult SuspendUserForever(string userId, bool suspend)
  {
    var applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);
    if (applicationUser != null)
    {
      if (suspend)
      {
        applicationUser.LockoutEnd = DateTime.Now.AddYears(100);
      }
      else
      {
        applicationUser.LockoutEnd = null;
      }
      _unitOfWork.ApplicationUser.Update(applicationUser);
      _unitOfWork.Save();
      return Json(new { success = true, message = "User suspended successfully!" });
    }
    else
    {
      return Json(new { success = false, message = "User suspended unsuccessfully!" });
    }
  }

  [HttpPost]
  public IActionResult SuspendUserForTime(string userId, double hours)
  {
    var applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);
    if (applicationUser != null)
    {
      applicationUser.LockoutEnd = DateTime.Now.AddHours(hours);
      _unitOfWork.ApplicationUser.Update(applicationUser);
      _unitOfWork.Save();
      return Json(new { success = true, message = "User suspended successfully!" });
    }
    else
    {
      return Json(new { success = false, message = "User suspended unsuccessfully!" });
    }
  }

  [HttpDelete]
  public IActionResult Delete(string userId)
  {
    var applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(p => p.Id == userId);

    if (applicationUser == null)
    {
      return Json(new { success = false, message = "Error while deleting!" });
    }
    else
    {
      _unitOfWork.ApplicationUser.Remove(applicationUser);
      _unitOfWork.Save();
      return Json(new { success = true, message = "User deleteed successful!" });
    }
  }
}
