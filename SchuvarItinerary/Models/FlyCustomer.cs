using System;
using System.Collections.Generic;

namespace SchuvarItinerary.Models;

public partial class FlyCustomer
{
    public int IdFly { get; set; }

    public int IdCustomer { get; set; }

    public int IdAerolinea { get; set; }

    public string Route { get; set; } = null!;

    public string Localizer { get; set; } = null!;

    public DateTime Departures { get; set; }

    public DateTime Arrivals { get; set; }

    public virtual Aerolinea IdAerolineaNavigation { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;
}
