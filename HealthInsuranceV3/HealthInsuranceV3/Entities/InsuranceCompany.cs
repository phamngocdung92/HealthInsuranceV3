using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class InsuranceCompany
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactInfo { get; set; }

    public string? Address { get; set; }

    public DateTime? EstablishedYear { get; set; }

    public bool? Dbstatus { get; set; }

    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
}
