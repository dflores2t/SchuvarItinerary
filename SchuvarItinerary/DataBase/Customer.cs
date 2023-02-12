using System;
using System.Collections.Generic;

namespace SchuvarItinerary.DataBase;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerFullname { get; set; } = null!;

    public string CustomerPhone { get; set; } = null!;

    public bool? CustomerIsdeleted { get; set; }

    public DateTime? CustomerDateup { get; set; }

    public DateTime? CustomerDatemodify { get; set; }

    public virtual ICollection<Flycustomer> Flycustomers { get; } = new List<Flycustomer>();
}
