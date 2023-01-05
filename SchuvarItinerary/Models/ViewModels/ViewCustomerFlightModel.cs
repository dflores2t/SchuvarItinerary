using System.ComponentModel.DataAnnotations;
namespace SchuvarItinerary.Models.ViewModels;

public class ViewCustomerFlightModel
{
  public int IdCustomer { get; set; }

  [Required(ErrorMessage = "This field is required")]
  [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "Passanger full name between 10 and 50 characters")]
  public string FullName { get; set; } = null!;
  [Required(ErrorMessage = "This field is required")]
  public int Contact { get; set; }

  public ViewFlightModel? Flight { get; set; }
}