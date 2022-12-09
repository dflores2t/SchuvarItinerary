namespace SchuvarItinerary.Models.ViewModels;

public class ViewCustomerFlightModel
{
  public int IdCustomer { get; set; }

  public string FullName { get; set; } = null!;

  public short Contact { get; set; }

  public ViewFlightModel? Flight { get; set; }
}