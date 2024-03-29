using System.ComponentModel.DataAnnotations;
namespace SchuvarItinerary.Models.ViewModels;

public class ViewCustomerFlightModel
{
  public int IdCustomer { get; set; }

  [Required(ErrorMessage = "This field is required")]
  [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Passanger full name between 10 and 100 characters")]
  public string CustomerFullName { get; set; } = null!;
  [Required(ErrorMessage = "This field is required")]
  [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "Invalid Phone Number.")]
  public string? CustomerPhone { get; set; }

  public ViewFlightModel? Flight { get; set; }
}