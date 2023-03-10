using System.Reflection;
using System.ComponentModel.DataAnnotations;
using SchuvarItinerary.DataBase;
namespace SchuvarItinerary.Models.ViewModels;

public class ViewAirLine
{
  public ViewAirLine()
  { }
  public ViewAirLine(Aerolinea model)
  {
    IdAerolinea = model.AerolineaId;
    AerolineaName = model.AerolineaShortname;
    AeroDescription = model.AerolineaFullname;
    AerolineaIsDeleted = model.AerolineaIsdeleted;
  }
  [Display(Name = "Id")]
  public int IdAerolinea { get; set; }
  [Display(Name = "Short Name")]
  [StringLength(maximumLength: 2, MinimumLength = 2, ErrorMessage = "Is necesary two caracter for short name!")]
  [Required(ErrorMessage = "This field is required!")]
  public string? AerolineaName { get; set; }
  [Display(Name = "Aero Line Description")]
  [Required(ErrorMessage = "This field is required!")]
  [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "String characters between 5 and 50")]
  public string? AeroDescription { get; set; }
  /// <summary>
  ///IsDelete return true if record is deleted
  /// </summary>
  public bool? AerolineaIsDeleted { get; set; } = false;
  public DateTime? AerolineaDateup { get; set; }

  public AerolineaFormsLink? FormsLink { get; set; }
}

public class AerolineaFormsLink
{
  public string? AerolineaIncomingForm { get; set; }
  public string? AerolineaOutgoingForm { get; set; }
}