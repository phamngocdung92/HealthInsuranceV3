using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Repositories.ForManagerRepository
{
    public interface IForManagerRepository
    {
        IEnumerable<ForManagerModel> GetEmployeeList(string Id, bool IsManager);
        IEnumerable<ForManagerModel> CheckEmpInsurance(string Id);
        void ApproveInsurance(int RegistrationId, string EmployeeId);
        IEnumerable<ForManagerModel> GetRejectionReasons();
        void RejectInsuranceRegistration(int RegistrationId, string EmployeeId, int ReasonId);
        IEnumerable<ForManagerModel> GetNewEmp(string Id, bool IsManager);
        void UpdateEmployeeDepartment(string Id, string EmployeeId, int DepartmentId);
        IEnumerable<ForManagerModel> GetDepartments();
        IEnumerable<ForManagerModel> GetManagers();
    }
}