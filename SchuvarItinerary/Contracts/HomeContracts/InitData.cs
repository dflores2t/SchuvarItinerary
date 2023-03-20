using System.Runtime.InteropServices;
using Newtonsoft.Json;
using SchuvarItinerary.DataBase;
using SchuvarItinerary.Models.ViewModels;

namespace SchuvarItinerary.Contracts.HomeContracts;
public static class InitData
{
  public static IQueryable<ViewFlyCustomerResult> IniData(SchuvaritineraryContext DbContext)
  {
    var data = from rows in DbContext.Flycustomers
               select new ViewFlyCustomerResult
               {
                 FlycustomerLocalyzer = rows.FlycustomerLocalyzer,
                 CustomerPhone = rows.FlycustomerIdcustomerNavigation.CustomerPhone,
                 CustomerFullName = rows.FlycustomerIdcustomerNavigation.CustomerFullname,
                 FlyCustomerDeparture = (DateTime)rows.FlycustomerDeparture!,
                 FlyCustomerArrivals = (DateTime)rows.FlycustomerArrivals!,
                 AerolineaFullname = rows.FlycustomerIdaerolineaNavigation.AerolineaFullname,
                 FlycustomerRoute = rows.FlycustomerRoute,
                 FormsLink = JsonConvert.DeserializeObject<AerolineaFormsLink>(rows.FlycustomerIdaerolineaNavigation.AerolineFormlinks!),
                 FlyCustomerId = rows.FlycustomerId
               };
    return data;
  }
}