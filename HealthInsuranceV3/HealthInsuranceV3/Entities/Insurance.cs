using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class Insurance
{
    public int InsuranceId { get; set; }

    public string InsuranceName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Dbstatus { get; set; }

    public int? CompanyId { get; set; }

    public string? IconText { get; set; }

    public virtual InsuranceCompany? Company { get; set; }

    public virtual ICollection<InsurancePackage> InsurancePackages { get; set; } = new List<InsurancePackage>();

    public virtual ICollection<InsuranceRegistration> InsuranceRegistrations { get; set; } = new List<InsuranceRegistration>();
}
