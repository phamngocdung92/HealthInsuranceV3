using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Services.ForManagerService
{
    public interface IForManagerService
    {
        IEnumerable<ForManagerModel> GetEmployeeList(string Id, bool IsManager);
        bool IsUserManager(string userId);
        IEnumerable<ForManagerModel> CheckEmpInsurance(string Id);
        void ApproveInsurance(int RegistrationId, string EmployeeId);
        IEnumerable<ForManagerModel> GetRejectionReasons();
        void RejectInsuranceRegistration(int RegistrationId, string EmployeeId, int RejectionReasonId);
    }
}
