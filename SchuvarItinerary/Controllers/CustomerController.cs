using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuvarItinerary.Models;
using SchuvarItinerary.Models.ViewModels;

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
  public ViewResult AddCustomerFlight()
  {
    ViewData["Aerolinea"] = new SelectList(dbContext.Aerolineas, "IdAerolinea", "AeroDescription");
    return View();
  }
  #endregion

  #region PostEndPoint
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> AddCustomerFlight(ViewCustomerFlightModel? model)
  {
    //validated if view model is valid
    if (!ModelState.IsValid)
    {
      ViewBag.message = "Data Invalid, Please try again!";
      return View(model);
    }

    Customer customer = new()
    {
      FullName = model!.FullName.ToUpper(),
      Contact = model!.Contact
    };
    try
    {
      if (!CustomerExist(model.Contact))
      {
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
      }
      else
      {
        customer = dbContext.Customers.FirstOrDefault(d => d.Contact == model.Contact)!;
      }
    }
    catch (DbUpdateConcurrencyException ex)
    {
      ViewBag.Error = ex.Message;
    }
    FlyCustomer customerItinerary = new()
    {
      IdCustomer = customer.IdCustomer,
      IdAerolinea = model.Flight!.IdAerolinea,
      Route = model.Flight.Route.ToUpper(),
      Localizer = model.Flight.Localizer.ToUpper(),
      Departures = model.Flight.Departures,
      Arrivals = model.Flight.Arrivals
    };
    try
    {
      dbContext.FlyCustomers.Add(customerItinerary);
      await dbContext.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException ex)
    {
      ViewBag.Error = ex.Message;
    }
    return RedirectToAction("Index", "Home");
  }
  [HttpPost]
  [ValidateAntiForgeryToken]
  public JsonResult FindCustomer(int phone)
  {
    //search if exitst customer by phone numbre
    var customer = from c in dbContext.Customers
                   select c;
    if (!string.IsNullOrEmpty(phone.ToString()))
    {
      customer = customer.Where(d => d.Contact == phone);
    }
    return Json(JsonConvert.SerializeObject(customer));
  }
  #endregion

  private bool CustomerExist(int phone)
  {
    return dbContext.Customers.Any(d => d.Contact == phone);
  }
}