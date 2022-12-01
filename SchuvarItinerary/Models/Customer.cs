using System;
using System.Collections.Generic;

namespace SchuvarItinerary.Models;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public string FullName { get; set; } = null!;

    public short Contact { get; set; }

    public virtual ICollection<FlyCustomer> FlyCustomers { get; } = new List<FlyCustomer>();
}
