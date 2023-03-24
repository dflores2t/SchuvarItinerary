using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuvarItinerary.Contracts.SettingsContracts;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;

namespace SchuvarItinerary.Controllers;

public class SettingsController : Controller
{
  private readonly SchuvaritineraryContext dbContext;
  private readonly AerolineaContracts _ac;

  public SettingsController(SchuvaritineraryContext dbContext)
  {
    this.dbContext = dbContext;
    _ac = new(dbContext);
  }

  #region GetEndPoint
  public async Task<ViewResult> Index()
  {
    return View(await _ac.IndexViewAirline());
  }
  [HttpGet] //trigger add page
  public ViewResult AirLine() => View();
  [HttpGet] //trigger edit page
  public async Task<IActionResult> UpdateAirline(int? id)
  {
    // Aerolinea? data = new();
    if (id == null)
    {
      return NotFound();
    }
    return View(await _ac.FindUpdateAirline(id));
  }

  #endregion

  #region PostEndPoint
  [HttpPost] //add new entry.
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> AirLine(ViewAirLine model)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.ErrorMessage = "Data Invalid. Please try again!";
      return View(model);
    }
    var status = await _ac.AddNewAirlineEntry(model);
    return status ? RedirectToAction(nameof(Index)) : View();
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

    if (IdAerolinea != model.IdAerolinea)
    {
      return NotFound();
    }

    Boolean status = false;
    try
    {
      status = await _ac.PostUpdateAirline(model);
      ViewBag.Success = $"{model.AerolineaName!.ToUpper()} Data Updated Succesfuly";
    }
    catch (DbUpdateException ex)
    {
      ViewBag.ErrorMessage = ex.Message;
    }
    return status ? RedirectToAction(nameof(Index)) : View(model);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteAirline(int? id)
  {
    Boolean status = false;
    try
    {
      status = await _ac.DeleteAirlineEntry(id);
    }
    catch (DbUpdateConcurrencyException ex)
    {
      ViewBag.ErrorMessage = ex.Message;
    }
    return status ? RedirectToAction(nameof(Index)) : View();
  }
  #endregion
}