using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class DepartmentManager
{
    public string EmployeeId { get; set; } = null!;

    public string? ManagerId { get; set; }

    public int? DepartmentId { get; set; }

    public bool IsManager { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Dbstatus { get; set; }

    public virtual Department? Department { get; set; }

    public virtual AspNetUser? Manager { get; set; }
}
