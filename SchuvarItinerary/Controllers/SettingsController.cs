using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuvarItinerary.Models;
using SchuvarItinerary.Models.ViewModels;

namespace SchuvarItinerary.Controllers;

public class SettingsController : Controller
{
  private readonly SchuvarItineraryContext dbContext;

  public SettingsController(SchuvarItineraryContext dbContext)
  {
    this.dbContext = dbContext;
  }

  #region GetEndPoint
  public async Task<ViewResult> Index()
  {
    var result = new List<ViewAirLine>();
    result = await dbContext.Aerolineas.Where(d => d.IsDeleted == false).Select(d => new ViewAirLine(d)).ToListAsync();
    return View(result);
  }
  [HttpGet]
  public ViewResult AirLine() => View();
  [HttpGet]
  public async Task<IActionResult> UpdateAirline(int? id)
  {
    Aerolinea? data = new();
    if (id == null)
    {
      return NotFound();
    }
    try
    {
      data = await dbContext.Aerolineas.FindAsync(id);
    }
    catch (System.Exception ex)
    {
      ViewBag.ErrorMessage = ex.Message;
    }
    ViewAirLine result = new()
    {
      IdAerolinea = data!.IdAerolinea,
      AerolineaName = data.AerolineaName.ToUpper(),
      AeroDescription = data.AeroDescription.ToUpper()
    };
    return View(result);
  }

  #endregion

  #region PostEndPoint
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> AirLine(ViewAirLine model)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.ErrorMessage = "Data Invalid. Please try again!";
      return View(model);
    }
    Aerolinea airLine = new()
    {
      AerolineaName = model.AerolineaName!.ToUpper(),
      AeroDescription = model.AeroDescription!.ToUpper(),
      IsDeleted = false
    };
    dbContext.Add(airLine);
    try
    {
      await dbContext.SaveChangesAsync();
      ViewBag.Success = $"{model.AerolineaName.ToUpper()} Data Save Successfuly";
    }
    catch (System.Exception ex)
    {
      ViewBag.ErrorMessage = ex.Message;
    }
    return RedirectToAction(nameof(Index));
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> UpdateAirline(ViewAirLine model)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.ErrorMessage = "Data Invalid. please try again!";
      return View(model);
    }
      Aerolinea airline = new()
      {
        IdAerolinea = model.IdAerolinea,
        AerolineaName = model.AerolineaName!.ToUpper(),
        AeroDescription = model.AeroDescription!.ToUpper(),
        IsDeleted = false
      };
      try
      {
        dbContext.Aerolineas.Update(airline);
        await dbContext.SaveChangesAsync();
        ViewBag.Success = $"{model.AerolineaName!.ToUpper()} Data Updated Succesfuly";
      }
      catch (DbUpdateException ex)
      {
        ViewBag.ErrorMessage = ex.Message;
      }
    return RedirectToAction(nameof(Index));
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteAirline(int? id)
  {
    Aerolinea? data = await dbContext.Aerolineas.FindAsync(id);
    data!.IsDeleted = true;
    try
    {
      dbContext.Aerolineas.Update(data);
      await dbContext.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException ex)
    {
      return !AerolineaExists(id) ? ViewBag.ErrorMessage = ex.Message : NotFound();
    }
    return RedirectToAction(nameof(Index));
  }
  #endregion

  private bool AerolineaExists(int? id)
  {
    return dbContext.Aerolineas.Any(e => e.IdAerolinea == id);
  }
}