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
    public class SecurityLoginsLogRepository :DBOperations, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Security_Logins_Log(Id,Source_IP,Logon_Date,Is_Succesful,Login) " +
                        "VALUES(@id,@sourceIP,@logonDate,@isSuccesful,@login)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@sourceIP", enitity.SourceIP);
                    cmd.Parameters.AddWithValue("@logonDate", enitity.LogonDate);
                    cmd.Parameters.AddWithValue("@isSuccesful", enitity.IsSuccesful);
                    cmd.Parameters.AddWithValue("@login", enitity.Login);


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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            var app_education = new List<SecurityLoginsLogPoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Security_Logins_Log", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_education.Add(new SecurityLoginsLogPoco()
                    {
                        Id = (Guid)rdr["Id"],
                        SourceIP = (string)rdr["Source_IP"],
                        LogonDate = (DateTime)rdr["Logon_Date"],
                        IsSuccesful = (bool)rdr["Is_Succesful"],
                        Login = (Guid)rdr["Login"]
                        

                    });
                }

                conn.Close();

                if (app_education == null)
                {
                    app_education = new List<SecurityLoginsLogPoco>();
                }

                return app_education;
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Security_Logins_Log WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Security_Logins_Log SET Source_IP = @sourceIP,Logon_Date = @logonDate, Is_Succesful = @isSuccesful WHERE ID = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@sourceIP", enitity.SourceIP);
                    cmd.Parameters.AddWithValue("@logonDate", enitity.LogonDate);
                    cmd.Parameters.AddWithValue("@isSuccesful", enitity.IsSuccesful);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
