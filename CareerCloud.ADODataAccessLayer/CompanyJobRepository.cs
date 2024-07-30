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
    public class CompanyJobRepository : DBOperations, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Company_Jobs(Id,Profile_Created,Is_Inactive,Is_Company_Hidden,Company) VALUES(@id,@profileCreated,@isInactive,@isCompanyHidden,@company)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@profileCreated", enitity.ProfileCreated);
                    cmd.Parameters.AddWithValue("@isInactive", enitity.IsInactive);
                    cmd.Parameters.AddWithValue("@isCompanyHidden", enitity.IsCompanyHidden);
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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            var app_education = new List<CompanyJobPoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Company_Jobs", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_education.Add(new CompanyJobPoco()
                    {
                        Id = (Guid)rdr["Id"],
                        ProfileCreated = (DateTime)rdr["Profile_Created"],
                        IsInactive = (bool)rdr["Is_Inactive"],
                        IsCompanyHidden = (bool)rdr["Is_Company_Hidden"],
                        Company = (Guid)rdr["Company"]
                        
                    });
                }

                conn.Close();

                if (app_education == null)
                {
                    app_education = new List<CompanyJobPoco>();
                }

                return app_education;
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Company_Jobs WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Jobs SET Profile_Created = @profileCreated,Is_Inactive = @isInactive, Is_Company_Hidden = @isCompanyHidden, Company = @company WHERE ID = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@profileCreated", enitity.ProfileCreated);
                    cmd.Parameters.AddWithValue("@isInactive", enitity.IsInactive);
                    cmd.Parameters.AddWithValue("@isCompanyHidden", enitity.IsCompanyHidden);
                    cmd.Parameters.AddWithValue("@company", enitity.Company);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
