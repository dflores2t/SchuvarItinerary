using System.Runtime.InteropServices;
using System.Data.Common;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;
using SchuvarItinerary.Contracts.CustomerContracts;

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

  public async Task<IActionResult> MontlyItinerary(DateTime? month)
  {
    if (dbContext.Flycustomers == null)
    {
      return Problem("Table FlyCustmoer is empthy");
    }

    var data = await CustomerContracts.GetMontlyItinerary(dbContext, month);

    if (data == null)
    {
      return NotFound();
    }

    return View(data);
  }

  public ViewResult AddCustomerFlight()
  {
    ViewData["Aerolinea"] = CustomerContracts.AerolineaDropDownList(dbContext);
    return View();
  }

  public async Task<IActionResult> UpdateCustomerItinerary(int? id)
  {
    ViewFlyCustomerResult result = new();
    if (id == null)
    {
      return NotFound();
    }
    try
    {
      result = await CustomerContracts.UpdateCustomerItinerary(dbContext, id);
    }
    catch (System.Exception ex)
    {
      ViewBag.Error = ex.Message;
    }
    ViewData["Aerolinea"] = CustomerContracts.AerolineaDropDownList(dbContext);
    return View(result);
  }
  #endregion

  #region PostEndPoint
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> AddCustomerFlight(ViewCustomerFlightModel? model)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.message = "Data Invalid, Please try again!";
      return View(model);
    }

    try
    {
      await CustomerContracts.NewCustomerItinerary(dbContext, model!);
      return RedirectToAction("Index", "Home");
    }
    catch (System.Exception ex)
    {
      ViewBag.Error = ex.Message;
    }
    return View(model);
  }
  [HttpPost]
  [ValidateAntiForgeryToken]
  public JsonResult FindCustomer(string phone)
  {
    try
    {
      return Json(CustomerContracts.FindCustomer(dbContext, phone));
    }
    catch (System.Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> UpdateCustomerItinerary(int id, [Bind("FlyCustomerId", "CustomerPhone", "CustomerFullName", "FlycustomerLocalyzer", "AerolineaId", "FlycustomerRoute", "FlyCustomerDeparture", "FlyCustomerArrivals")] ViewFlyCustomerResult model)
  {
    Boolean result = false;

    if (id != model.FlyCustomerId)
    {
      return NotFound();
    }
    if (ModelState.IsValid)
    {
      try
      {
        result = await CustomerContracts.SaveChangesCustomerItinerary(dbContext, model);
      }
      catch (System.Exception)
      {
        throw;
      }
    }
    return result ? RedirectToAction(nameof(MontlyItinerary)) : View(model);
  }

  public async Task<IActionResult> DeleteCustomerItinerary(int? id)
  {
    Boolean result = await CustomerContracts.DeleteCustomerItinerary(dbContext, id);
    return result ? RedirectToAction(nameof(MontlyItinerary)) : View();
  }
  #endregion
}