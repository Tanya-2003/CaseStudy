using System;
using System.Collections.Generic;

namespace Car_Rental_System.Models;

public partial class Lease
{
    public int LeaseId { get; set; }

    public int? VehicleId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? LeaseType { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Vehicle? Vehicle { get; set; }
}
