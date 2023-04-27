﻿using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Managers;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.Utilities;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class YerbaMateController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ShoppingCartManager<YerbaMate, YerbaMateShoppingCart> _shoppingCartManager;

  public YerbaMateController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
    _shoppingCartManager = new(unitOfWork);
  }

  [HttpGet]
  public IActionResult Index(string type, string country, string brand, string weight)
  {
    YerbaMateProductsViewModel YerbaMateVM = new(_unitOfWork, type, country, brand, weight);
    return View(YerbaMateVM);
  }

  [HttpGet]
  public IActionResult Details(int productId)
  {
    YerbaMateShoppingCart shoppingCart = _shoppingCartManager.CreateShoppingCart(productId);
    Country country = _unitOfWork.YerbaMate.GetFirstOrDefault(p => p.Id == productId, includeProperties: "Country").Country;
    ViewData["CountryName"] = country.Name;
    ViewData["CountryCode"] = country.CountryIsoAlfa2Code;
    return View(shoppingCart);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Details(YerbaMateShoppingCart shoppingCart)
  {
    if (ModelState.IsValid)
    {
      string userClaimsValue = UserClaims.GetUserClaimsValue(User);
      shoppingCart.ApplicationUserId = userClaimsValue;

      var shoppingCartFromDb = _unitOfWork.YerbaMateShoppingCart.GetFirstOrDefault(
          i => i.ApplicationUserId == userClaimsValue && i.ProductId == shoppingCart.ProductId);
      _shoppingCartManager.AddOrIncrement(shoppingCart, shoppingCartFromDb);
      _unitOfWork.Save();

      TempData["success"] = "Product added to cart!";
      return RedirectToAction(nameof(Index));
    }
    else
    {
      TempData["error"] = "Something went wrong!";
      return RedirectToAction(nameof(Details), new { productId = shoppingCart.ProductId });
    }
  }

  [HttpPost]
  public IActionResult AddToCart(int productId)
  {
    YerbaMateShoppingCart shoppingCart = _shoppingCartManager.CreateShoppingCart(productId, 1);
      string userClaimsValue = UserClaims.GetUserClaimsValue(User);
      shoppingCart.ApplicationUserId = userClaimsValue;

    if (shoppingCart.Product != null)
    {
      var shoppingCartFromDb = _unitOfWork.YerbaMateShoppingCart.GetFirstOrDefault(
          i => i.ApplicationUserId == userClaimsValue && i.ProductId == shoppingCart.ProductId);
      _shoppingCartManager.AddOrIncrement(shoppingCart, shoppingCartFromDb);
      _unitOfWork.Save();

      return Json(new { success = true, message = "Product added to cart!" });
    }
    else
    {
      return Json(new { success = false, message = "Something went wrong!" });
    }
  }


}
