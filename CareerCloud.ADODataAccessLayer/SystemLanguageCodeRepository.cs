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
    public class SystemLanguageCodeRepository : DBOperations, IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO System_Language_Codes(LanguageID,Native_Name,Name) VALUES(@languageID,@nativeName,@name)";

                    cmd.Parameters.AddWithValue("@languageID", enitity.LanguageID);
                    cmd.Parameters.AddWithValue("@nativeName", enitity.NativeName);
                    cmd.Parameters.AddWithValue("@name", enitity.Name);
                    
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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            var app_education = new List<SystemLanguageCodePoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from System_Language_Codes", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_education.Add(new SystemLanguageCodePoco()
                    {
                        LanguageID = (string)rdr["LanguageID"],
                        NativeName = (string)rdr["Native_Name"],
                        Name = (string)rdr["Name"]
                        
                    });
                }

                conn.Close();

                if (app_education == null)
                {
                    app_education = new List<SystemLanguageCodePoco>();
                }

                return app_education;
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM System_Language_Codes WHERE LanguageID = @languageID";

                    cmd.Parameters.AddWithValue("@languageID", enitity.LanguageID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE System_Language_Codes SET Native_Name = @nativeName, Name = @name WHERE LanguageID = @languageID";

                    cmd.Parameters.AddWithValue("@languageID", enitity.LanguageID);
                    cmd.Parameters.AddWithValue("@nativeName", enitity.NativeName);
                    cmd.Parameters.AddWithValue("@name", enitity.Name);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
