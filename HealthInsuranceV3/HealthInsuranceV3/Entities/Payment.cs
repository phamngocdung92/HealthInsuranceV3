using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? RegistrationId { get; set; }

    public decimal? PaymentAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? PaymentStatusId { get; set; }

    public bool? Dbstatus { get; set; }

    public virtual PaymentStatus? PaymentStatus { get; set; }

    public virtual InsuranceRegistration? Registration { get; set; }
}
