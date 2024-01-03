using HealthInsuranceV3.Data;
using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HealthInsuranceV3.Areas.User.Repositories.InsuranceDetailRepository
{
	public class InsuranceDetailRepository : IInsuranceDetailRepository
	{
		private readonly HealthInsuranceV3Context _context;
		public InsuranceDetailRepository(HealthInsuranceV3Context context)
		{
			_context = context;
		}
		public IEnumerable<InsuranceDetailModel> GetInsuranceDetail(int InsuranceId)
		{
			List<InsuranceDetailModel> InsuranceDetail = new List<InsuranceDetailModel>();

			using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("GetInsuranceDetail", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					// Pass the EmployeeID as a parameter
					//command.Parameters.AddWithValue("@EmployeeID", employeeID);
					command.Parameters.AddWithValue("@InsuranceId", InsuranceId);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							InsuranceDetailModel insuranceDetail = new InsuranceDetailModel
							{
								IconText = reader.GetString(reader.GetOrdinal("IconText")),
								PackageName = reader.GetString(reader.GetOrdinal("PackageName")),
								CoverageDetails = reader.GetString(reader.GetOrdinal("CoverageDetails")),
								CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
								ContactInfo = reader.GetString(reader.GetOrdinal("ContactInfo")),
								Address = reader.GetString(reader.GetOrdinal("Address")),
								EstablishedYear = reader.GetDateTime(reader.GetOrdinal("EstablishedYear")),
								PolicyTermInDay = reader.GetInt32(reader.GetOrdinal("PolicyTermInDay")),
								Price = reader.GetDecimal(reader.GetOrdinal("Price")),
								PackageId = reader.GetInt32(reader.GetOrdinal("PackageId")),
							};
                            insuranceDetail.EstablishedYearFormatted = string.Format("{0:dd/MM/yyyy}", insuranceDetail.EstablishedYear);
                            InsuranceDetail.Add(insuranceDetail);
						}
					}
				}
			}

			return InsuranceDetail;
		}

        public void RegisterInsuranceForEmployee(string UserId, int PackageId)
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("RegisterInsuranceForEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Pass the parameters to the stored procedure
                    command.Parameters.AddWithValue("@Id", UserId);
                    command.Parameters.AddWithValue("@PackageId", PackageId);

                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log errors, or throw further if needed
                        Console.WriteLine($"Error registering insurance for employee: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public IEnumerable<InsuranceDetailModel> GetPackageDetail(int PackageId)
        {
            List<InsuranceDetailModel> InsuranceDetail = new List<InsuranceDetailModel>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetPackageDetail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Pass the EmployeeID as a parameter
                    //command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    command.Parameters.AddWithValue("@PackageId", PackageId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InsuranceDetailModel insuranceDetail = new InsuranceDetailModel
                            {
                                IconText = reader.GetString(reader.GetOrdinal("IconText")),
                                PackageName = reader.GetString(reader.GetOrdinal("PackageName")),
                                CoverageDetails = reader.GetString(reader.GetOrdinal("CoverageDetails")),
                                CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                                ContactInfo = reader.GetString(reader.GetOrdinal("ContactInfo")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                EstablishedYear = reader.GetDateTime(reader.GetOrdinal("EstablishedYear")),
                                PolicyTermInDay = reader.GetInt32(reader.GetOrdinal("PolicyTermInDay")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                PackageId = reader.GetInt32(reader.GetOrdinal("PackageId")),
                            };
                            insuranceDetail.EstablishedYearFormatted = string.Format("{0:dd/MM/yyyy}", insuranceDetail.EstablishedYear);
                            InsuranceDetail.Add(insuranceDetail);
                        }
                    }
                }
            }

            return InsuranceDetail;
        }
    }
}
