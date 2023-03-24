using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;

namespace SchuvarItinerary.Contracts.SettingsContracts;

public class AerolineaContracts
{
  private readonly SchuvaritineraryContext dbcontext;

  public AerolineaContracts(SchuvaritineraryContext dbcontext)
  {
    this.dbcontext = dbcontext;
  }
  /// <summary>
  ///  This method print all record on table Aeorlines
  /// </summary>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<List<ViewAirLine>> IndexViewAirline()
  {
    var indexViewairlineView = new List<ViewAirLine>();
    try
    {
      indexViewairlineView = await dbcontext.Aerolineas.Where(d => d.AerolineaIsdeleted == false).Select(d => new ViewAirLine(d)).ToListAsync();
    }
    catch (System.Exception ex)
    {
      throw new Exception(ex.Message);
    }
    return indexViewairlineView;
  }
  /// <summary>
  ///  This method is used to find and print data on update page
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<ViewAirLine> FindUpdateAirline(int? id)
  {
    ViewAirLine airlineView = new();
    try
    {
      Aerolinea? airlineData = await dbcontext.Aerolineas.FindAsync(id);
      airlineView.IdAerolinea = airlineData!.AerolineaId;
      airlineView.AerolineaName = airlineData.AerolineaShortname.ToUpper();
      airlineView.AeroDescription = airlineData.AerolineaFullname.ToUpper();
      airlineView.AerolineaDateup = airlineData.AerolineaDateup;
      airlineView.AerolineaIsDeleted = airlineData.AerolineaIsdeleted;
      airlineView.FormsLink = JsonConvert.DeserializeObject<AerolineaFormsLink>(airlineData.AerolineFormlinks!)!;
    }
    catch (System.Exception ex)
    {
      throw new Exception(ex.Message);
    }
    return airlineView;
  }
  /// <summary>
  ///  This method is used to get information from update airline page and update the record modified.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public async Task<Boolean> PostUpdateAirline(ViewAirLine model)
  {
    // this object is used to get individual information from input page and convert in json.
    AerolineaFormsLink FormsLink = new()
    {
      AerolineaIncomingForm = model.FormsLink!.AerolineaIncomingForm,
      AerolineaOutgoingForm = model.FormsLink!.AerolineaOutgoingForm
    };
    Aerolinea aerolineaUpdate = await dbcontext.Aerolineas.FirstAsync(a => a.AerolineaId == model.IdAerolinea);
    aerolineaUpdate.AerolineaShortname = model.AerolineaName!.ToUpper();
    aerolineaUpdate.AerolineaFullname = model.AeroDescription!.ToUpper();
    aerolineaUpdate.AerolineFormlinks = JsonConvert.SerializeObject(FormsLink);
    aerolineaUpdate.AerolineaDatemodify = DateTime.Now;
    Boolean status = false;
    try
    {
      await dbcontext.SaveChangesAsync();
      status = true;
    }
    catch (DbUpdateException ex)
    {
      throw new Exception(ex.Message);
    }
    return status;
  }
  /// <summary>
  /// This method is used to add new airline entry.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  public async Task<Boolean> AddNewAirlineEntry(ViewAirLine model)
  {
    AerolineaFormsLink formsLink = new()
    {
      AerolineaIncomingForm = model.FormsLink!.AerolineaIncomingForm,
      AerolineaOutgoingForm = model.FormsLink!.AerolineaOutgoingForm
    };
    Aerolinea airLineNewEntry = new()
    {
      AerolineaShortname = model.AerolineaName!.ToUpper(),
      AerolineaFullname = model.AeroDescription!.ToUpper(),
      AerolineFormlinks = JsonConvert.SerializeObject(formsLink),
      AerolineaIsdeleted = false
    };
    dbcontext.Add(airLineNewEntry);
    bool status;
    try
    {
      await dbcontext.SaveChangesAsync();
      status = true;
    }
    catch (System.Exception)
    {
      throw;
    }
    return status;
  }
/// <summary>
/// This method is used to set property isDeleted to true, and simulate thant record is deleted .
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
public async Task<Boolean> DeleteAirlineEntry(int? id){
    var aerolineaDelete = await dbcontext.Aerolineas.FindAsync(id);
    aerolineaDelete!.AerolineaIsdeleted = true;
    aerolineaDelete.AerolineaDatemodify = DateTime.Now;
    bool status;
    try
    {
      await dbcontext.SaveChangesAsync();
      status = true;
    }
    catch (DbUpdateException ex)
    {
      throw new Exception(ex.Message);
    }
    return status;
  }
}