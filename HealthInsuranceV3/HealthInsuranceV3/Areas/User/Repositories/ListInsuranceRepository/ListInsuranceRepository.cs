using HealthInsuranceV3.Data;
using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository
{
    public class ListInsuranceRepository : IListInsuranceRepository
    {
        private readonly HealthInsuranceContext _context;
        public ListInsuranceRepository(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IEnumerable<ListInsuranceModel> GetInsuranceList()
        {
            List<ListInsuranceModel> InsuranceList = new List<ListInsuranceModel>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetInsuranceList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Pass the EmployeeID as a parameter
                    //command.Parameters.AddWithValue("@EmployeeID", employeeID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListInsuranceModel listInsurance = new ListInsuranceModel
                            {
                                InsuranceId = reader.GetInt32(reader.GetOrdinal("InsuranceId")),
                                IconText = reader.GetString(reader.GetOrdinal("IconText")),
                                InsuranceName = reader.GetString(reader.GetOrdinal("InsuranceName")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                                ContactInfo = reader.GetString(reader.GetOrdinal("ContactInfo")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                EstablishedYear = reader.GetDateTime(reader.GetOrdinal("EstablishedYear"))
                            };
                            listInsurance.EstablishedYearFormatted = string.Format("{0:dd/MM/yyyy}", listInsurance.EstablishedYear);
                            InsuranceList.Add(listInsurance);
                        }
                    }
                }
            }

            return InsuranceList;
        }
    }
}