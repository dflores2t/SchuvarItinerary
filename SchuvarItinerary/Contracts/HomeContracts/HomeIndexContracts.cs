using System;
using Microsoft.EntityFrameworkCore;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;
using SchuvarItinerary.Contracts.HomeContracts;

namespace SchuvarItinerary.Contracts;

public static class HomeIndexContracts
{
  //Today- passangers
  public static async Task<List<ViewFlyCustomerResult>> GetItineraryNow(SchuvaritineraryContext DbContext)
  {
    string today = @DateTime.Now.AddDays(3).ToShortDateString();
    var data = InitData.IniData(DbContext);
    data = data
    .Where(d => d.FlyCustomerDeparture == DateTimeOffset.Parse(today).UtcDateTime);
    return await data.ToListAsync();
  }

  public static async Task<List<ViewFlyCustomerResult>> GetFilterItinerary(SchuvaritineraryContext DbContext,string searchString){
    var data = InitData.IniData(DbContext).Where(s=> s.CustomerFullName!.Contains(searchString.ToUpper()));
return await data.ToListAsync();
  }
}