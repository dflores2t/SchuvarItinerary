using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;

namespace SchuvarItinerary.Controllers;

public class SettingsController : Controller
{
  private readonly SchuvaritineraryContext dbContext;

  public SettingsController(SchuvaritineraryContext dbContext)
  {
    this.dbContext = dbContext;
  }

  #region GetEndPoint
  public async Task<ViewResult> Index()
  {
    var result = new List<ViewAirLine>();
    result = await dbContext.Aerolineas.Where(d => d.AerolineaIsdeleted == false).Select(d => new ViewAirLine(d)).ToListAsync();
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
    catch (Exception ex)
    {
      ViewBag.ErrorMessage = ex.Message;
    }
    ViewAirLine result = new()
    {
      IdAerolinea = data!.AerolineaId,
      AerolineaName = data.AerolineaShortname.ToUpper(),
      AeroDescription = data.AerolineaFullname.ToUpper(),
      AerolineaDateup = data.AerolineaDateup,
      AerolineaIsDeleted = data.AerolineaIsdeleted,
      FormsLink = JsonConvert.DeserializeObject<AerolineaFormsLink>(data.AerolineFormlinks!)!
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
    AerolineaFormsLink FormsLink = new()
    {
      AerolineaIncomingForm = model.FormsLink!.AerolineaIncomingForm,
      AerolineaOutgoingForm = model.FormsLink!.AerolineaOutgoingForm
    };
    Aerolinea airLine = new()
    {
      AerolineaShortname = model.AerolineaName!.ToUpper(),
      AerolineaFullname = model.AeroDescription!.ToUpper(),
      AerolineFormlinks = JsonConvert.SerializeObject(FormsLink),
      AerolineaIsdeleted = false
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
  public async Task<IActionResult> UpdateAirline(int IdAerolinea, [Bind("IdAerolinea,AerolineaName,AeroDescription,FormsLink,AerolieneaIsDeleted,AerolineaDateup")] ViewAirLine model)
  {
    if (IdAerolinea != model.IdAerolinea)
    {
      return NotFound();
    }
    if (!ModelState.IsValid)
    {
      ViewBag.ErrorMessage = "Data Invalid. please try again!";
      return View(model);
    }

    AerolineaFormsLink FormsLink = new()
    {
      AerolineaIncomingForm = model.FormsLink!.AerolineaIncomingForm,
      AerolineaOutgoingForm = model.FormsLink!.AerolineaOutgoingForm
    };

    Aerolinea aerolineaUpdate = new()
    {
      AerolineaId = model.IdAerolinea,
      AerolineaShortname = model.AerolineaName!.ToUpper(),
      AerolineaFullname = model.AeroDescription!.ToUpper(),
      AerolineFormlinks = JsonConvert.SerializeObject(FormsLink),
      AerolineaDateup = model.AerolineaDateup,
      AerolineaDatemodify = DateTime.Now,
      AerolineaIsdeleted = model.AerolineaIsDeleted
    };
    try
    {
      dbContext.Update(aerolineaUpdate);
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
    data!.AerolineaIsdeleted = true;
    data.AerolineaDatemodify = DateTime.Now;
    try
    {
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
    return dbContext.Aerolineas.Any(e => e.AerolineaId == id);
  }
}