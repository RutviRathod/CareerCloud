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
    public class ApplicantSkillRepository : DBOperations, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Applicant_Skills(Id,Skill_Level,Start_Month,Start_Year,End_Month,End_Year,Applicant,Skill) " +
                                                     "VALUES(@id,@skillLevel,@startMonth,@startYear,@endMonth,@endYear,@applicant,@skill)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@skillLevel", enitity.SkillLevel);
                    cmd.Parameters.AddWithValue("@startMonth", enitity.StartMonth);
                    cmd.Parameters.AddWithValue("@startYear", enitity.StartYear);
                    cmd.Parameters.AddWithValue("@endMonth", enitity.EndMonth);
                    cmd.Parameters.AddWithValue("@endYear", enitity.EndYear);
                    cmd.Parameters.AddWithValue("@applicant", enitity.Applicant);
                    cmd.Parameters.AddWithValue("@skill", enitity.Skill);

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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            var app_skill = new List<ApplicantSkillPoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Applicant_Skills", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_skill.Add(new ApplicantSkillPoco()
                    {
                        Id = (Guid)rdr["Id"],
                        SkillLevel = (string)rdr["Skill_Level"],
                        StartMonth = (byte)rdr["Start_Month"],
                        StartYear = (int)rdr["Start_Year"],
                        EndMonth = (byte)rdr["End_Month"],
                        EndYear = (int)rdr["End_Year"],
                        TimeStamp = (byte[])rdr["Time_Stamp"],
                        Applicant = (Guid)rdr["Applicant"],
                        Skill = (string)rdr["Skill"],


                    });
                }

                conn.Close();

                if (app_skill == null)
                {
                    app_skill = new List<ApplicantSkillPoco>();
                }

                return app_skill;
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Applicant_Skills WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Applicant_Skills SET Skill_Level = @skillLevel,Start_Month = @startMonth, " +
                                                        "Start_Year = @startYear,End_Month = @endMonth," +
                                                        "End_Year = @endYear,Applicant = @applicant,Skill = @skill  WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@skillLevel", enitity.SkillLevel);
                    cmd.Parameters.AddWithValue("@startMonth", enitity.StartMonth);
                    cmd.Parameters.AddWithValue("@startYear", enitity.StartYear);
                    cmd.Parameters.AddWithValue("@endMonth", enitity.EndMonth);
                    cmd.Parameters.AddWithValue("@endYear", enitity.EndYear);
                    cmd.Parameters.AddWithValue("@applicant", enitity.Applicant);
                    cmd.Parameters.AddWithValue("@skill", enitity.Skill);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
