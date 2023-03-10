using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;

namespace SchuvarItinerary.Controllers;

public class CustomerController : Controller
{
  //DI Db connection.
  private readonly SchuvaritineraryContext dbContext;

  public CustomerController(SchuvaritineraryContext DbContext)
  {
    dbContext = DbContext;
  }

  #region GetEndPoint
  public ViewResult AddCustomerFlight()
  {
    ViewData["Aerolinea"] = new SelectList(dbContext.Aerolineas, "AerolineaId", "AerolineaFullname");
    return View();
  }

  public async Task<ViewResult> MontlyItinerary()
  {
    return View(await dbContext.Flycustomers.Include(c => c.FlycustomerIdcustomerNavigation)
    .Include(a => a.FlycustomerIdaerolineaNavigation).ToListAsync());
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
      CustomerFullname = model!.CustomerFullName.ToUpper(),
      CustomerPhone = model.CustomerPhone!
    };
    try
    {
      if (!CustomerExist(model.CustomerPhone!))
      {
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
      }
      else
      {
        customer = dbContext.Customers.FirstOrDefault(d => d.CustomerPhone == model.CustomerPhone)!;
      }
    }
    catch (DbUpdateConcurrencyException ex)
    {
      ViewBag.Error = ex.Message;
    }
    Flycustomer customerItinerary = new()
    {
      FlycustomerIdcustomer = customer.CustomerId,
      FlycustomerIdaerolinea = model.Flight!.IdAerolinea,
      FlycustomerRoute = model.Flight.Route.ToUpper(),
      FlycustomerLocalyzer = model.Flight.Localizer.ToUpper(),
      FlycustomerDeparture = DateTimeOffset.Parse(model.Flight.Departures.ToString()).UtcDateTime,
      FlycustomerArrivals = DateTimeOffset.Parse(model.Flight.Arrivals.ToString()).UtcDateTime,
    };
    try
    {
      dbContext.Flycustomers.Add(customerItinerary);
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
  public JsonResult FindCustomer(string phone)
  {
    //search if exitst customer by phone numbre
    var customer = from c in dbContext.Customers
                   select c;

    if (!string.IsNullOrEmpty(phone))
    {
      customer = customer.Where(d => d.CustomerPhone == phone);
    }
    return Json(JsonConvert.SerializeObject(customer));
  }
  #endregion

  private bool CustomerExist(string phone)
  {
    return dbContext.Customers.Any(d => d.CustomerPhone == phone);
  }
}