using System;
using System.Collections.Generic;

namespace Car_Rental_System.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
}
