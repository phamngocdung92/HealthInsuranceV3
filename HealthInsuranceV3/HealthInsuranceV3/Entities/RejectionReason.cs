using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class RejectionReason
{
    public int ReasonId { get; set; }

    public string ReasonText { get; set; } = null!;

    public bool? Dbstatus { get; set; }

    public virtual ICollection<InsuranceRegistration> InsuranceRegistrations { get; set; } = new List<InsuranceRegistration>();
}
