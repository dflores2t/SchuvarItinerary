namespace SchuvarItinerary.Models.ViewModels;

public class ViewFlightModel
{
  public int IdFly { get; set; }

  public int IdCustomer { get; set; }

  public int IdAerolinea { get; set; }

  public string Route { get; set; } = null!;

  public string Localizer { get; set; } = null!;

  public DateTime Departures { get; set; }

  public DateTime Arrivals { get; set; }
}