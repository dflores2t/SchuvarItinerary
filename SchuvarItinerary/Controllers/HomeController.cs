using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models;
using SchuvarItinerary.Models.ViewModels;

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
    if(dbContext.Flycustomers == null){
      return Problem("Table FlyCustomer is emthy");
    }
    var data = from dd in dbContext.Flycustomers
               select dd;

    if(!String.IsNullOrEmpty(searchString)){
      data = data.Where(s => s.FlycustomerIdcustomerNavigation.CustomerFullname.Contains(searchString))
      .Include(c => c.FlycustomerIdcustomerNavigation)
      .Include(a => a.FlycustomerIdaerolineaNavigation);
    }

    string today = @DateTime.Now.AddDays(3).ToShortDateString(); //ToShortDateString();
    try
    {
      // var flyCustomerResult = dbContext.Flycustomers
      data = data
      .Where(d => d.FlycustomerDeparture == DateTimeOffset.Parse(today).UtcDateTime)
      .Include(c => c.FlycustomerIdcustomerNavigation)
      .Include(a => a.FlycustomerIdaerolineaNavigation);
      // return View(await flyCustomerResult.ToListAsync());
      return View(await data.ToListAsync());
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
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