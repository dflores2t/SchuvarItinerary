using System.ComponentModel;

namespace SchuvarItinerary.Models.ViewModels;

public class ViewFlyCustomerResult
{
  public int FlyCustomerId { get; set; }
  public string? FlycustomerLocalyzer { get; set; }
  public string? CustomerPhone { get; set; }
  public string? CustomerFullName { get; set; }
  public DateTime FlyCustomerDeparture { get; set; }
  public DateTime FlyCustomerArrivals { get; set; }
  public string? AerolineaFullname { get; set; }
  public string? FlycustomerRoute { get; set; }
  public AerolineaFormsLink? FormsLink { get; set; }
}