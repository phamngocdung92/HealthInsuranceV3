using HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository;
using HealthInsuranceV3.Data;
using HealthInsuranceV3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HealthInsuranceV3.Areas.User.Repositories.InsuranceDetailRepository
{
	public interface IInsuranceDetailRepository
	{
		IEnumerable<InsuranceDetailModel> GetInsuranceDetail(int InsuranceId);
        void RegisterInsuranceForEmployee(string UserId, int PackageId);
        IEnumerable<InsuranceDetailModel> GetPackageDetail(int PackageId);
    }
}
