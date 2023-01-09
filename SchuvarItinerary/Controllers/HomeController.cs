using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuvarItinerary.Models;
using SchuvarItinerary.Models.ViewModels;

namespace SchuvarItinerary.Controllers;

public class HomeController : Controller
{
  private readonly SchuvarItineraryContext dbContext;

  public HomeController(SchuvarItineraryContext dbContext)
  {
    this.dbContext = dbContext;
  }

  public async Task<IActionResult> Index()
  {
    var flyCustomerResult = dbContext.FlyCustomers.Include(c => c.IdCustomerNavigation)
    .Include(a => a.IdAerolineaNavigation);

    return View(await flyCustomerResult.ToListAsync());
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
