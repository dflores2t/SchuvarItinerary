using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchuvarItinerary.Models;

namespace SchuvarItinerary.Controllers;

public class CustomerController : Controller
{
  //DI Db connection.
  private readonly SchuvarItineraryContext dbContext;

  public CustomerController(SchuvarItineraryContext DbContext)
  {
    dbContext = DbContext;
  }

  #region GetEndPoint
  public IActionResult AddCustomerFlight()
  {
    ViewData["Aerolinea"] = new SelectList(dbContext.Aerolineas, "IdAerolinea", "AeroDescription");
    return View();
  }
  #endregion
}