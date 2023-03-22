using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;

namespace SchuvarItinerary.Contracts.CustomerContracts;

public static class CustomerContracts
{
  /// <summary>
  /// This method get all records that departure date is equal to Datetime.Now
  /// </summary>
  /// <param name="DbContext"></param>
  /// <param name="month"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public static async Task<List<Flycustomer>> GetMontlyItinerary(SchuvaritineraryContext DbContext, DateTime? month)
  {
    month ??= DateTime.Now;
    var data = from rows in DbContext.Flycustomers
               select rows;
    if (data != null)
    {
      try
      {
        data = data
         .Where(d => d.FlycustomerDeparture!.Value.Year == month.Value.Year && d.FlycustomerDeparture.Value.Month == month.Value.Month)
         .Include(c => c.FlycustomerIdcustomerNavigation)
         .Include(a => a.FlycustomerIdaerolineaNavigation);
      }
      catch (System.Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
    return await data!.ToListAsync();
  }

  /// <summary>
  /// This method is used to fill out dropdownlist on add customel views
  /// </summary>
  /// <param name="DbContext"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public static SelectList AerolineaDropDownList(SchuvaritineraryContext DbContext)
  {
    try
    {
      return new SelectList(DbContext.Aerolineas, "AerolineaId", "AerolineaFullname");
    }
    catch (System.Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
/// <summary>
/// This method, save all data to database
/// First check if the customer exist, if exists only keep the infomation, else, add new record into flycustomer object.
/// </summary>
/// <param name="DbContext"></param>
/// <param name="model"></param>
/// <returns></returns>
/// <exception cref="Exception"></exception>
  public static async Task<Boolean> NewCustomerItinerary(SchuvaritineraryContext DbContext, ViewCustomerFlightModel model)
  {
    Customer? customer = new()
    {
      CustomerFullname = model.CustomerFullName.ToUpper(),
      CustomerPhone = model.CustomerPhone!
    };

    if (DbContext.Customers.Any(d => d.CustomerPhone == customer.CustomerPhone))
    {
      customer = DbContext.Customers.FirstOrDefault(c => c.CustomerPhone == model.CustomerPhone)!;
    }

    Flycustomer customerItinerary = new()
    {
      FlycustomerIdaerolinea = model.Flight!.IdAerolinea,
      FlycustomerRoute = model.Flight.Route.ToUpper(),
      FlycustomerLocalyzer = model.Flight.Localizer.ToUpper(),
      FlycustomerDeparture = DateTimeOffset.Parse(model.Flight.Departures.ToString()).UtcDateTime,
      FlycustomerArrivals = DateTimeOffset.Parse(model.Flight.Arrivals.ToString()).UtcDateTime,
      FlycustomerIdcustomerNavigation = customer
    };
    try
    {
      DbContext.Flycustomers.Add(customerItinerary);
      await DbContext.SaveChangesAsync();
    }
    catch (DbUpdateException ex)
    {
      throw new Exception(ex.Message);
    }
    return true;
  }
/// <summary>
/// This method is used to fill input name customer if the phone number exists into records.
/// </summary>
/// <param name="DbContext"></param>
/// <param name="phone"></param>
/// <returns></returns>
  public static string FindCustomer(SchuvaritineraryContext DbContext, string phone)
  {
    var customer = from c in DbContext.Customers
                   select c;
    if (!string.IsNullOrEmpty(phone))
    {
      customer = customer.Where(d => d.CustomerPhone == phone);
    }
    return JsonConvert.SerializeObject(customer);
  }
}