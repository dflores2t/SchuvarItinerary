using Microsoft.AspNetCore.Mvc;
using SchuvarItinerary.Models;

namespace SchuvarItinerary.Controllers;

public class SettingsController : Controller
{
  private readonly SchuvarItineraryContext dbContex;

  public SettingsController(SchuvarItineraryContext dbContex)
  {
    this.dbContex = dbContex;
  }

  #region GetEndPoint
  public IActionResult Settings(){
    return View();
  }

  #endregion

  #region PostEndPoint

  #endregion
}