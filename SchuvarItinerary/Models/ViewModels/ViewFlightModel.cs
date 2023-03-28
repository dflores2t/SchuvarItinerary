using System.ComponentModel.DataAnnotations;
using SchuvarItinerary.Infrastructure;

namespace SchuvarItinerary.Models.ViewModels;

public class ViewFlightModel
{
  public int IdFly { get; set; }

  public int IdCustomer { get; set; }
  [Required(ErrorMessage = "This fiel cannot be empty!")]
  [Range(1, int.MaxValue, ErrorMessage = "Please select an Aeroline!")]
  public int IdAerolinea { get; set; }

  [Required]
  [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Route string greater than 5 characters")]
  public string Route { get; set; } = null!;

  [Required(ErrorMessage = "This field is required!")]
  [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "The localizer need to be 6 characater")]
  public string Localizer { get; set; } = null!;

  [Required(ErrorMessage = "This field is required!")]
  [DataType(DataType.Date)]
  public DateTime Departures { get; set; }
  [DataType(DataType.Date)]
  public DateTime Arrivals { get; set; }
}