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
    public class CompanyLocationRepository :  DBOperations,IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Company_Locations(Id,Country_Code,State_Province_Code,Street_Address,City_Town,Zip_Postal_Code,Company ) VALUES(@id,@countryCode,@province,@street,@city,@postalCode,@company)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@countryCode", enitity.CountryCode);
                    cmd.Parameters.AddWithValue("@province", enitity.Province);
                    cmd.Parameters.AddWithValue("@street", enitity.Street);
                    cmd.Parameters.AddWithValue("@city", enitity.City);
                    cmd.Parameters.AddWithValue("@postalCode", enitity.PostalCode);
                    cmd.Parameters.AddWithValue("@company", enitity.Company);

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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            var app_education = new List<CompanyLocationPoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Company_Locations", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_education.Add(new CompanyLocationPoco()
                    {
                        Id = (Guid)rdr["Id"],
                        CountryCode = rdr.IsDBNull(rdr.GetOrdinal("Country_Code")) ? null : (string)rdr["Country_Code"],
                        Province = rdr.IsDBNull(rdr.GetOrdinal("State_Province_Code")) ? null : (string)rdr["State_Province_Code"],
                        Street = rdr.IsDBNull(rdr.GetOrdinal("Street_Address")) ? null : (string)rdr["Street_Address"],
                        City = rdr.IsDBNull(rdr.GetOrdinal("City_Town")) ? null : (string)rdr["City_Town"],
                        PostalCode = rdr.IsDBNull(rdr.GetOrdinal("Zip_Postal_Code")) ? null : (string)rdr["Zip_Postal_Code"],
                        Company = (Guid)rdr["Company"]

                    });
                }

                conn.Close();

                if (app_education == null)
                {
                    app_education = new List<CompanyLocationPoco>();
                }

                return app_education;
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Company_Locations WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Locations SET Country_Code = @countryCode,State_Province_Code = @province, Street_Address = @street,City_Town = @city,Zip_Postal_Code = @postalCode WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@countryCode", enitity.CountryCode);
                    cmd.Parameters.AddWithValue("@province", enitity.Province);
                    cmd.Parameters.AddWithValue("@street", enitity.Street);
                    cmd.Parameters.AddWithValue("@city", enitity.City);
                    cmd.Parameters.AddWithValue("@postalCode", enitity.PostalCode);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
