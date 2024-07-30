using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : DBOperations ,IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Applicant_Profiles(Id,Current_Salary,Current_Rate,Country_Code,State_Province_Code,Street_Address,City_Town,Zip_Postal_Code, Login, Currency ) " +
                        "VALUES(@id,@currentSalary,@currentRate,@countryCode,@stateProvinceCode,@streetAddress,@cityTown, @zipPostalCode,@login, @currency)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@currentSalary", enitity.CurrentSalary);
                    cmd.Parameters.AddWithValue("@currentRate", enitity.CurrentRate);
                    cmd.Parameters.AddWithValue("@countryCode", enitity.Country);
                    cmd.Parameters.AddWithValue("@stateProvinceCode", enitity.Province);
                    cmd.Parameters.AddWithValue("@streetAddress", enitity.Street);
                    cmd.Parameters.AddWithValue("@cityTown", enitity.City);
                    cmd.Parameters.AddWithValue("@zipPostalCode", enitity.PostalCode);
                    cmd.Parameters.AddWithValue("@login", enitity.Login);
                    cmd.Parameters.AddWithValue("@currency", enitity.Currency);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            var app_education = new List<ApplicantProfilePoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Applicant_Profiles", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_education.Add(new ApplicantProfilePoco()
                    {
                        Id = (Guid)rdr["Id"],
                        CurrentSalary = (decimal)rdr["Current_Salary"],
                        CurrentRate = (decimal)rdr["Current_Rate"],
                        Country = (string)rdr["Country_Code"],
                        Province = (string)rdr["State_Province_Code"],
                        Street = (string)rdr["Street_Address"],
                        City = (string)rdr["City_Town"],
                        PostalCode = (string)rdr["Zip_Postal_Code"],
                        TimeStamp = (byte[])rdr["Time_Stamp"],
                        Login = (Guid)rdr["Login"],
                        Currency = (string)rdr["Currency"]

                    });
                }

                conn.Close();

                if (app_education == null)
                {
                    app_education = new List<ApplicantProfilePoco>();
                }

                return app_education;
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Applicant_Profiles WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Applicant_Profiles SET Current_Salary = @currentSalary,Current_Rate=@currentRate,Country_Code=@countryCode,State_Province_Code=@stateProvinceCode,Street_Address=@streetAddress,City_Town=@cityTown,Zip_Postal_Code=@zipPostalCode, Login=@login, Currency=@currency WHERE Id = @Id";
                                                                                                                                                                                                 

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@currentSalary", enitity.CurrentSalary);
                    cmd.Parameters.AddWithValue("@currentRate", enitity.CurrentRate);
                    cmd.Parameters.AddWithValue("@countryCode", enitity.Country);
                    cmd.Parameters.AddWithValue("@stateProvinceCode", enitity.Province);
                    cmd.Parameters.AddWithValue("@streetAddress", enitity.Street);
                    cmd.Parameters.AddWithValue("@cityTown", enitity.City);
                    cmd.Parameters.AddWithValue("@zipPostalCode", enitity.PostalCode);
                    cmd.Parameters.AddWithValue("@login", enitity.Login);
                    cmd.Parameters.AddWithValue("@currency", enitity.Currency);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
