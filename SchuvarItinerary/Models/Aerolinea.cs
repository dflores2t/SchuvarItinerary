using System;
using System.Collections.Generic;

namespace SchuvarItinerary.Models;

public partial class Aerolinea
{
    public int IdAerolinea { get; set; }

    public string AerolineaName { get; set; } = null!;

    public string AeroDescription { get; set; } = null!;

    public virtual ICollection<FlyCustomer> FlyCustomers { get; } = new List<FlyCustomer>();
}
