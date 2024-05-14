using System;
using System.Collections.Generic;

namespace Car_Rental_System.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }

    public int? DailyRate { get; set; }

    public string? Status { get; set; }

    public int? PassengerCapacity { get; set; }

    public int? EngineCapacity { get; set; }

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
}
