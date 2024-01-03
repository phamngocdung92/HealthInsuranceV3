using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class PaymentStatus
{
    public int PaymentStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public bool? Dbstatus { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
