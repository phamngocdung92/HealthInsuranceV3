using HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository;
using HealthInsuranceV3.Data;
using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HealthInsuranceV3.Areas.User.Repositories.UserInsuranceManagerRepository
{
    public class UserInsuranceManagerRepository : IUserInsuranceManagerRepository
    {
        private readonly HealthInsuranceContext _context;
        public UserInsuranceManagerRepository(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IEnumerable<UserInsuranceManagerModel> GetUserInsuranceRegistrations(string Id)
        {
            List<UserInsuranceManagerModel> UserInsuranceManager = new List<UserInsuranceManagerModel>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetUserInsuranceRegistrations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Pass the EmployeeID as a parameter
                    command.Parameters.AddWithValue("@Id", Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserInsuranceManagerModel userInsuranceManager = new UserInsuranceManagerModel
                            {
                                RegistrationId = reader.GetInt32(reader.GetOrdinal("RegistrationId")),
                                EmployeeId = reader.GetString(reader.GetOrdinal("EmployeeId")),
                                InsuranceId = reader.GetInt32(reader.GetOrdinal("InsuranceId")),
                                PackageId = reader.GetInt32(reader.GetOrdinal("PackageId")),
                                PackageName = reader.GetString(reader.GetOrdinal("PackageName")),
                                RegistrationStatus = reader.GetString(reader.GetOrdinal("RegistrationStatus")),
                                RejectionReasonId = reader.IsDBNull(reader.GetOrdinal("RejectionReasonId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RejectionReasonId")),
                                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate"))
                            };
                            userInsuranceManager.RegistrationDateFormatted = string.Format("{0:dd/MM/yyyy}", userInsuranceManager.RegistrationDate);
                            userInsuranceManager.ApprovalDateFormatted = userInsuranceManager.ApprovalDate.HasValue
                                ? string.Format("{0:dd/MM/yyyy}", userInsuranceManager.ApprovalDate)
                                : string.Empty; // Handle the case where ApprovalDate is null

                            UserInsuranceManager.Add(userInsuranceManager);
                        }
                    }
                }
            }

            return UserInsuranceManager;
        }

        public void DeleteUserInsuranceRegistration(string Id, int RegistrationId)
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteUserInsuranceRegistration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Pass the parameters to the stored procedure
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@RegistrationId", RegistrationId);

                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log errors, or throw further if needed
                        Console.WriteLine($"Error delete insurance registration for employee: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}
