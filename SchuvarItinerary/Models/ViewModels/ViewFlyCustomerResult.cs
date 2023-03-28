using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchuvarItinerary.Models.ViewModels;

public class ViewFlyCustomerResult
{
  public int FlyCustomerId { get; set; }
  [Required(ErrorMessage = "This field is required!")]
  [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "The localizer need to be 6 characater")]
  public string? FlycustomerLocalyzer { get; set; }
  public string? CustomerPhone { get; set; }
  public string? CustomerFullName { get; set; }
  [Range(1, int.MaxValue, ErrorMessage = "Please select an Aeroline!")]
  public int AerolineaId { get; set; }
  [DataType(DataType.Date)]
  public DateTime FlyCustomerDeparture { get; set; }
  [DataType(DataType.Date)]
  public DateTime FlyCustomerArrivals { get; set; }
  public string? AerolineaFullname { get; set; }
  [Required]
  [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Route string greater than 5 characters")]
  public string? FlycustomerRoute { get; set; }
  public AerolineaFormsLink? FormsLink { get; set; }
  public Boolean? FlycustomerFilled { get; set; }
  public Boolean? FlycustomerIsDeleted { get; set; }
}