using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class InsurancePackage
{
    public int PackageId { get; set; }

    public int? InsuranceId { get; set; }

    public string PackageName { get; set; } = null!;

    public string? CoverageDetails { get; set; }

    public int? PolicyTermInDay { get; set; }

    public bool? Dbstatus { get; set; }

    public decimal? Price { get; set; }

    public virtual Insurance? Insurance { get; set; }
}
