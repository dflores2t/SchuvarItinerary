using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Customer(){
    return View();
  }
  #endregion
}