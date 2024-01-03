using HealthInsuranceV3.Areas.User.Repositories.ForManagerRepository;
using HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository;
using HealthInsuranceV3.Areas.User.Services.ListInsuranceService;
using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Services.ForManagerService
{
    public class ForManagerService : IForManagerService
    {
        private readonly IForManagerRepository _ForManagerRepository;
        private readonly HealthInsuranceContext _dbContext;
        public ForManagerService(IForManagerRepository ForManagerRepository, HealthInsuranceContext dbContext)
        {
            _ForManagerRepository = ForManagerRepository;
            _dbContext = dbContext;
        }
        public IEnumerable<ForManagerModel> GetEmployeeList(string Id, bool IsManager)
        {
            return _ForManagerRepository.GetEmployeeList(Id, IsManager);
        }
        public bool IsUserManager(string Id)
        {
            // Thực hiện truy vấn để kiểm tra xem người dùng có là quản lý không
            return _dbContext.DepartmentManagers.Any(dm => dm.EmployeeId == Id && dm.IsManager);
        }
        public IEnumerable<ForManagerModel> CheckEmpInsurance(string Id)
        {
            return _ForManagerRepository.CheckEmpInsurance(Id);
        }
        public void ApproveInsurance(int RegistrationId, string EmployeeId)
        {
            _ForManagerRepository.ApproveInsurance(RegistrationId, EmployeeId);
        }
        public IEnumerable<ForManagerModel> GetRejectionReasons()
        {
            return _ForManagerRepository.GetRejectionReasons();
        }
        public void RejectInsuranceRegistration(int RegistrationId, string EmployeeId, int RejectionReasonId)
        { 
            _ForManagerRepository.RejectInsuranceRegistration(RegistrationId, EmployeeId, RejectionReasonId);
        }
        public IEnumerable<ForManagerModel> GetNewEmp(string Id, bool IsManager)
        {
            return _ForManagerRepository.GetNewEmp(Id, IsManager);
        }
        public void UpdateEmployeeDepartment(string Id, string EmployeeId, int ManagerId, int DepartmentId)
        {
            _ForManagerRepository.UpdateEmployeeDepartment(Id, EmployeeId, ManagerId, DepartmentId);
        }
        public IEnumerable<ForManagerModel> GetDepartments()
        {
            return _ForManagerRepository.GetDepartments();
        }
        public IEnumerable<ForManagerModel> GetManagers()
        {
            return _ForManagerRepository.GetManagers();
        }
    }
}
