using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuvarItinerary.Contracts.HomeContracts;
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
         .Where(d => d.FlycustomerDeparture!.Value.Year == month.Value.Year && d.FlycustomerDeparture.Value.Month == month.Value.Month && d.FlycustomerIsdeleted==false)
         .OrderByDescending(f => f.FlycustomerDeparture)
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
  /// <summary>
  /// This method return data from id to show on update page.
  /// </summary>
  /// <param name="DbContext"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  public static async Task<ViewFlyCustomerResult> UpdateCustomerItinerary(SchuvaritineraryContext DbContext, int? id)
  {
    var data = InitData.IniData(DbContext);
    data = data
    .Where(d => d.FlyCustomerId == id);
    return await data.FirstAsync();
  }
  /// <summary>
  ///  This resume get all data from update page and send new data to edit.
  /// </summary>
  /// <param name="DbContext"></param>
  /// <param name="customerItinerary"></param>
  /// <returns></returns>
  public static async Task<Boolean> SaveChangesCustomerItinerary(SchuvaritineraryContext DbContext, ViewFlyCustomerResult customerItinerary)
  {
    Boolean status = false;
    Customer customerUpdate = new()
    {
      CustomerFullname = customerItinerary.CustomerFullName!.ToUpper(),
      CustomerPhone = customerItinerary.CustomerPhone!,
    };
    if (DbContext.Customers.Any(c => c.CustomerPhone == customerUpdate.CustomerPhone))
    {
      customerUpdate = await DbContext.Customers
      .FirstAsync(c => c.CustomerPhone == customerItinerary.CustomerPhone);
    }
    var updateChanges = await DbContext.Flycustomers.FindAsync(customerItinerary.FlyCustomerId);
    updateChanges!.FlycustomerId = customerItinerary.FlyCustomerId;
    updateChanges.FlycustomerRoute = customerItinerary.FlycustomerRoute!.ToUpper();
    updateChanges.FlycustomerLocalyzer = customerItinerary.FlycustomerLocalyzer!.ToUpper();
    updateChanges.FlycustomerDeparture = DateTimeOffset.Parse(customerItinerary.FlyCustomerDeparture.ToString()).UtcDateTime;
    updateChanges.FlycustomerArrivals = DateTimeOffset.Parse(customerItinerary.FlyCustomerArrivals.ToString()).UtcDateTime;
    updateChanges.FlycustomerIdaerolinea = customerItinerary.AerolineaId;
    updateChanges.FlycustomerDatemodify = DateTime.Now;
    updateChanges.FlycustomerIdcustomerNavigation = customerUpdate;

    try
    {
      await DbContext.SaveChangesAsync();
      status = true;
    }
    catch (System.Exception)
    {
      status = false;
      throw;
    }
    return status;
  }
/// <summary>
/// This method put the property isdeleted in true.
/// </summary>
/// <param name="DbContext"></param>
/// <param name="id"></param>
/// <returns></returns>
  public static async Task<Boolean> DeleteCustomerItinerary(SchuvaritineraryContext DbContext, int? id)
  {
    Flycustomer? flyCustomer = await DbContext.Flycustomers.FindAsync(id);
    flyCustomer!.FlycustomerIsdeleted = true;
    flyCustomer.FlycustomerDatemodify = DateTime.Now;
    bool status;
    try
    {
      await DbContext.SaveChangesAsync();
      status = true;
    }
    catch (System.Exception)
    {
      throw;
    }
    return status;
  }
}