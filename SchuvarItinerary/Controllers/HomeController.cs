using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models;
using SchuvarItinerary.Contracts;

namespace SchuvarItinerary.Controllers;

public class HomeController : Controller
{
  private readonly SchuvaritineraryContext dbContext;

  public HomeController(SchuvaritineraryContext dbContext)
  {
    this.dbContext = dbContext;
  }

  public async Task<IActionResult> Index(string searchString)
  {
    if (dbContext.Flycustomers == null)
    {
      return Problem("Table FlyCustomer is empthy");
    }

    if (!String.IsNullOrEmpty(searchString))
    {
      return View(await HomeIndexContracts.GetFilterItinerary(dbContext, searchString));
    }

    return View(await HomeIndexContracts.GetItineraryNow(dbContext));
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

  [HttpPost]
  public async Task<StatusCodeResult> IsFilled(int? id)
  {
    if (await HomeIndexContracts.Filled(dbContext, id))
    {
      return StatusCode(StatusCodes.Status201Created);
    }
    return StatusCode(StatusCodes.Status401Unauthorized);
  }
}