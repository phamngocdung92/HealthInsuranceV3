using System;
using System.Collections.Generic;

namespace HealthInsuranceV3.Entities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int? Dbstatus { get; set; }

    public virtual ICollection<DepartmentManager> DepartmentManagers { get; set; } = new List<DepartmentManager>();
}
