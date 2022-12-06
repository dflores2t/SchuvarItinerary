﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuvarItinerary.Models;

namespace SchuvarItinerary.Controllers;

public class HomeController : Controller
{
  private readonly SchuvarItineraryContext context;

  public HomeController(SchuvarItineraryContext context)
  {
    this.context = context;
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
