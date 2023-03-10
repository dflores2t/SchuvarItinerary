using System;
using System.Collections.Generic;

namespace SchuvarItinerary.DataBase;

public partial class Flycustomer
{
    public int FlycustomerId { get; set; }

    public int FlycustomerIdcustomer { get; set; }

    public int FlycustomerIdaerolinea { get; set; }

    public string FlycustomerRoute { get; set; } = null!;

    public string FlycustomerLocalyzer { get; set; } = null!;

    public DateTime? FlycustomerDeparture { get; set; }

    public DateTime? FlycustomerArrivals { get; set; }

    public bool? FlycustomerFilled { get; set; }

    public bool? FlycustomerIsdeleted { get; set; }

    public DateTime? FlycustomerDateup { get; set; }

    public DateTime? FlycustomerDatemodify { get; set; }

    public virtual Aerolinea FlycustomerIdaerolineaNavigation { get; set; } = null!;

    public virtual Customer FlycustomerIdcustomerNavigation { get; set; } = null!;
}
