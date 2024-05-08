using System;
using System.Collections.Generic;

namespace Car_Rental_System.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? LeaseId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? Amount { get; set; }

    public virtual Lease? Lease { get; set; }
}
