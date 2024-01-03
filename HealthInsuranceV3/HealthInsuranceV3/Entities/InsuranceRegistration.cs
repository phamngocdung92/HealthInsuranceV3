using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class InsuranceRegistration
{
    public int RegistrationId { get; set; }

    public string? EmployeeId { get; set; }

    public int? InsuranceId { get; set; }

    public string? RegistrationStatus { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public int? RejectionReasonId { get; set; }

    public bool? Dbstatus { get; set; }

    public virtual AspNetUser? Employee { get; set; }

    public virtual Insurance? Insurance { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual RejectionReason? RejectionReason { get; set; }
}
