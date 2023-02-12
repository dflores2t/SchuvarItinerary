using System;
using System.Collections.Generic;

namespace SchuvarItinerary.DataBase;

public partial class Aerolinea
{
    public int AerolineaId { get; set; }

    public string AerolineaShortname { get; set; } = null!;

    public string AerolineaFullname { get; set; } = null!;

    public bool? AerolineaIsdeleted { get; set; }

    public DateTime? AerolineaDateup { get; set; }

    public DateTime? AerolineaDatemodify { get; set; }

    public virtual ICollection<Flycustomer> Flycustomers { get; } = new List<Flycustomer>();
}
