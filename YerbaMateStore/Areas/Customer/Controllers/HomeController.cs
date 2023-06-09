﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Repository.IRepository;
using YerbaMateStore.Models.ViewModels;

namespace YerbaMateStore.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly IUnitOfWork _unitOfWork;

  public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
  {
    _logger = logger;
    _unitOfWork = unitOfWork;
  }

  public IActionResult Index()
  {
    return View();
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
