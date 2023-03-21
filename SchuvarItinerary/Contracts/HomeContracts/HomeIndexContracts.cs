using Microsoft.EntityFrameworkCore;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;
using SchuvarItinerary.Contracts.HomeContracts;

namespace SchuvarItinerary.Contracts;

public static class HomeIndexContracts
{
  //Today- passangers
  /// <summary>
  /// This method return a Itinerary list where departure date is equal to now
  /// </summary>
  /// <param name="DbContext"></param>
  /// <returns></returns>
  public static async Task<List<ViewFlyCustomerResult>> GetItineraryNow(SchuvaritineraryContext DbContext)
  {
    string today = @DateTime.Now.AddDays(3).ToShortDateString();
    var data = InitData.IniData(DbContext);
    data = data
    .Where(d => d.FlyCustomerDeparture == DateTimeOffset.Parse(today).UtcDateTime && d.FlycustomerFilled == false);
    return await data.ToListAsync();
  }

  /// <summary>
  /// This method return a list filtered by name
  /// </summary>
  /// <param name="DbContext"></param>
  /// <param name="searchString"></param>
  /// <returns></returns>
  public static async Task<List<ViewFlyCustomerResult>> GetFilterItinerary(SchuvaritineraryContext DbContext, string searchString)
  {
    var data = InitData.IniData(DbContext).Where(s => s.CustomerFullName!.Contains(searchString.ToUpper()));
    return await data.ToListAsync();
  }

  //Toggle on to mark card as full filled
  /// <summary>
  /// This method changed the property flycustomerFilled to true when the switch is estable on the card view.
  /// </summary>
  /// <param name="DbContext"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public static async Task<Boolean> Filled(SchuvaritineraryContext DbContext, int? id)
  {
    Boolean ok = false;
    if (id > 0)
    {
      var data = DbContext.Flycustomers.Find(id);
      try
      {
        data!.FlycustomerFilled = true;
        data!.FlycustomerDatemodify = DateTime.Now;
        await DbContext.SaveChangesAsync();
        ok = true;
      }
      catch (DbUpdateException ex)
      {
        throw new Exception(ex.Message);
      }
    }
    return ok;
  }
}