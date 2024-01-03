using HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository;
using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HealthInsuranceV3.Areas.User.Repositories.ForManagerRepository
{
    public class ForManagerRepository : IForManagerRepository
    {
        private readonly HealthInsuranceContext _context;
        public ForManagerRepository(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IEnumerable<ForManagerModel> GetEmployeeList(string Id, bool IsManager)
        {
            List<ForManagerModel> EmployeeList = new List<ForManagerModel>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetEmployeeList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Pass the EmployeeID as a parameter
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@IsManager", IsManager);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ForManagerModel employeeList = new ForManagerModel
                            {
                                Id = reader.GetString(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? (string?)null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                DepartmentName = reader.IsDBNull(reader.GetOrdinal("DepartmentName")) ? (string?)null : reader.GetString(reader.GetOrdinal("DepartmentName")),
                            };
                            
                            EmployeeList.Add(employeeList);
                        }
                    }
                }
            }

            return EmployeeList;
        }

        public IEnumerable<ForManagerModel> CheckEmpInsurance(string Id)
        {
            List<ForManagerModel> EmpInsurance = new List<ForManagerModel>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetInsuranceRegistrations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Pass the EmployeeID as a parameter
                    command.Parameters.AddWithValue("@Id", Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ForManagerModel empInsurance = new ForManagerModel
                            {
                                RegistrationId = reader.GetInt32(reader.GetOrdinal("RegistrationId")),
                                EmployeeId = reader.GetString(reader.GetOrdinal("EmployeeId")),
                                InsuranceId = reader.GetInt32(reader.GetOrdinal("InsuranceId")),
                                PackageId = reader.GetInt32(reader.GetOrdinal("PackageId")),
                                PackageName = reader.GetString(reader.GetOrdinal("PackageName")),
                                RegistrationStatus = reader.IsDBNull(reader.GetOrdinal("RegistrationStatus")) ? (string?)null : reader.GetString(reader.GetOrdinal("RegistrationStatus")),
                                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate")),
                                RejectionReasonId = reader.IsDBNull(reader.GetOrdinal("RejectionReasonId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RejectionReasonId")),

                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            };

                            EmpInsurance.Add(empInsurance);
                        }
                    }
                }
            }

            return EmpInsurance;
        }

        public void ApproveInsurance(int RegistrationId, string EmployeeId)
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ApproveInsuranceRegistration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Pass the parameters to the stored procedure
                    command.Parameters.AddWithValue("@RegistrationId", RegistrationId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);

                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log errors, or throw further if needed
                        Console.WriteLine($"Error ApproveInsuranceRegistration for employee: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public IEnumerable<ForManagerModel> GetRejectionReasons()
        {
            List<ForManagerModel> RejectionReasons = new List<ForManagerModel>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetRejectionReasons", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Pass the EmployeeID as a parameter
                    //command.Parameters.AddWithValue("@EmployeeID", employeeID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ForManagerModel rejectionReasons = new ForManagerModel
                            {
                                ReasonId = reader.GetInt32(reader.GetOrdinal("ReasonId")),
                                ReasonText = reader.GetString(reader.GetOrdinal("ReasonText")),
                            };
                           
                            RejectionReasons.Add(rejectionReasons);
                        }
                    }
                }
            }

            return RejectionReasons;
        }

        public void RejectInsuranceRegistration(int RegistrationId, string EmployeeId, int RejectionReasonId)
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("RejectInsuranceRegistration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Pass the parameters to the stored procedure
                    command.Parameters.AddWithValue("@RegistrationId", RegistrationId);
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    command.Parameters.AddWithValue("@RejectionReasonId", RejectionReasonId);

                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log errors, or throw further if needed
                        Console.WriteLine($"Error RejectInsuranceRegistration for employee: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}