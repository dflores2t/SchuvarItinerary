using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchuvarItinerary.DataBase;

public partial class Aerolinea
{
    public int AerolineaId { get; set; }
    [DisplayName("Airline")]
    public string AerolineaShortname { get; set; } = null!;

    public string AerolineaFullname { get; set; } = null!;

    public string? AerolineFormlinks { get; set; }

    public bool? AerolineaIsdeleted { get; set; }

    public DateTime? AerolineaDateup { get; set; }

    public DateTime? AerolineaDatemodify { get; set; }

    public virtual ICollection<Flycustomer> Flycustomers { get; } = new List<Flycustomer>();
}
