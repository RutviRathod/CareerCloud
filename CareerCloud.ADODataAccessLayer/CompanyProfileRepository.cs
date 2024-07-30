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
    public class CompanyProfileRepository : DBOperations, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Company_Profiles(Id,Registration_Date,Company_Website,Contact_Phone,Contact_Name,Company_Logo) " +
                        "VALUES(@id,@registrationDate,@companyWebsite,@contactPhone,@contactName,@companyLogo)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@registrationDate", enitity.RegistrationDate);
                    cmd.Parameters.AddWithValue("@companyWebsite", enitity.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@contactPhone", enitity.ContactPhone);
                    cmd.Parameters.AddWithValue("@contactName", enitity.ContactName);
                    cmd.Parameters.AddWithValue("@companyLogo", enitity.CompanyLogo);
                    
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            var app_education = new List<CompanyProfilePoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Company_Profiles", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_education.Add(new CompanyProfilePoco()
                    {
                        Id = (Guid)rdr["Id"],
                        RegistrationDate = (DateTime)rdr["Registration_Date"],
                        CompanyWebsite = rdr.IsDBNull(rdr.GetOrdinal("Company_Website")) ? null : (string)rdr["Company_Website"],
                        ContactPhone = rdr.IsDBNull(rdr.GetOrdinal("Contact_Phone")) ? null : (string)rdr["Contact_Phone"],
                        ContactName = rdr.IsDBNull(rdr.GetOrdinal("Contact_Name")) ? null : (string)rdr["Contact_Name"],
                        CompanyLogo = rdr.IsDBNull(rdr.GetOrdinal("Company_Logo")) ? null : (byte[])rdr["Company_Logo"]
                                              
                                            
                    });
                }

                conn.Close();

                if (app_education == null)
                {
                    app_education = new List<CompanyProfilePoco>();
                }

                return app_education;
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Company_Profiles WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Profiles SET Registration_Date = @registrationDate,Company_Website = @companyWebsite, Contact_Phone = @contactPhone,Contact_Name = @contactName,Company_Logo = @companyLogo WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@registrationDate", enitity.RegistrationDate);
                    cmd.Parameters.AddWithValue("@companyWebsite", enitity.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@contactPhone", enitity.ContactPhone);
                    cmd.Parameters.AddWithValue("@contactName", enitity.ContactName);
                    cmd.Parameters.AddWithValue("@companyLogo", enitity.CompanyLogo);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
