using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SchuvarItinerary.DataBase;

public partial class Flycustomer
{
    public int FlycustomerId { get; set; }

    public int FlycustomerIdcustomer { get; set; }

    public int FlycustomerIdaerolinea { get; set; }
[DisplayName("Route")]
    public string FlycustomerRoute { get; set; } = null!;
[DisplayName("Localyze")]
    public string FlycustomerLocalyzer { get; set; } = null!;
    [DisplayName("Departure Date")]
    public DateTime? FlycustomerDeparture { get; set; }
[DisplayName("Arrivals Date")]
    public DateTime? FlycustomerArrivals { get; set; }

    public bool? FlycustomerFilled { get; set; }

    public bool? FlycustomerIsdeleted { get; set; }

    public DateTime? FlycustomerDateup { get; set; }

    public DateTime? FlycustomerDatemodify { get; set; }

    public virtual Aerolinea FlycustomerIdaerolineaNavigation { get; set; } = null!;

    public virtual Customer FlycustomerIdcustomerNavigation { get; set; } = null!;
}
