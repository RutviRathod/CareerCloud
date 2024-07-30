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
    public class CompanyJobSkillRepository : DBOperations, IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Company_Job_Skills(Id,Skill_Level,Skill,Importance,Job) VALUES(@id,@skillLevel,@skill,@importance,@job)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@skillLevel", enitity.SkillLevel);
                    cmd.Parameters.AddWithValue("@skill", enitity.Skill);
                    cmd.Parameters.AddWithValue("@importance", enitity.Importance);
                    cmd.Parameters.AddWithValue("@job", enitity.Job);

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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            var app_education = new List<CompanyJobSkillPoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Company_Job_Skills", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_education.Add(new CompanyJobSkillPoco()
                    {
                        Id = (Guid)rdr["Id"],
                        SkillLevel = (string)rdr["Skill_Level"],
                        TimeStamp = (byte[])rdr["Time_Stamp"],
                        Skill = (string)rdr["Skill"],
                        Importance = (int)rdr["Importance"],
                        Job = (Guid)rdr["Job"]
                    });
                }

                conn.Close();

                if (app_education == null)
                {
                    app_education = new List<CompanyJobSkillPoco>();
                }

                return app_education;
            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Company_Job_Skills WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Job_Skills SET Skill_Level = @skillLevel,Skill = @skill,Importance = @importance, Job =@job  WHERE ID = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@skillLevel", enitity.SkillLevel);
                    cmd.Parameters.AddWithValue("@skill", enitity.Skill);
                    cmd.Parameters.AddWithValue("@importance", enitity.Importance);
                    cmd.Parameters.AddWithValue("@job", enitity.Job);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
