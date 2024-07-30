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
    public class ApplicantWorkHistoryRepository : DBOperations, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Applicant_Work_History(Id,Company_Name,Country_Code,Job_Title,Job_Description,Start_Month,Start_Year,End_Month,End_Year,Applicant,Location) VALUES(@id,@companyName,@countryCode,@jobTitle,@jobDescription,@startMonth,@startYear,@endMonth,@endYear,@applicant,@location)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@companyName", enitity.CompanyName);
                    cmd.Parameters.AddWithValue("@countryCode", enitity.CountryCode);
                    cmd.Parameters.AddWithValue("@jobTitle", enitity.JobTitle);
                    cmd.Parameters.AddWithValue("@jobDescription", enitity.JobDescription);
                    cmd.Parameters.AddWithValue("@startMonth", enitity.StartMonth);
                    cmd.Parameters.AddWithValue("@startYear", enitity.StartYear);
                    cmd.Parameters.AddWithValue("@endMonth", enitity.EndMonth);
                    cmd.Parameters.AddWithValue("@endYear", enitity.EndYear);
                    cmd.Parameters.AddWithValue("@applicant", enitity.Applicant);
                    cmd.Parameters.AddWithValue("@location", enitity.Location);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            var app_work = new List<ApplicantWorkHistoryPoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Applicant_Work_History", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_work.Add(new ApplicantWorkHistoryPoco()
                    {
                        Id = (Guid)rdr["Id"],
                        CompanyName = (string)rdr["Company_Name"],
                        CountryCode = (string)rdr["Country_Code"],
                        JobTitle = (string)rdr["Job_Title"],
                        JobDescription = (string)rdr["Job_Description"],
                        StartMonth = (short)rdr["Start_Month"],
                        StartYear = (int)rdr["Start_Year"],
                        EndMonth = (short)rdr["End_Month"],
                        EndYear = (int)rdr["End_Year"],
                        TimeStamp = (byte[])rdr["Time_Stamp"],
                        Applicant = (Guid)rdr["Applicant"],
                        Location = (string)rdr["Location"]

                    });
                }

                conn.Close();

                if (app_work == null)
                {
                    app_work = new List<ApplicantWorkHistoryPoco>();
                }

                return app_work;
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Applicant_Work_History WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Applicant_Work_History SET Company_Name = @companyName,Country_Code = @countryCode, Job_Title = @jobTitle,Job_Description = @jobDescription,Start_Month = @startMonth," +
                        "Start_Year = @startYear, End_Month=@endMonth, End_Year=@endYear, Applicant = @applicant, Location=@location WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@companyName", enitity.CompanyName);
                    cmd.Parameters.AddWithValue("@countryCode", enitity.CountryCode);
                    cmd.Parameters.AddWithValue("@jobTitle", enitity.JobTitle);
                    cmd.Parameters.AddWithValue("@jobDescription", enitity.JobDescription);
                    cmd.Parameters.AddWithValue("@startMonth", enitity.StartMonth);
                    cmd.Parameters.AddWithValue("@startYear", enitity.StartYear);
                    cmd.Parameters.AddWithValue("@endMonth", enitity.EndMonth);
                    cmd.Parameters.AddWithValue("@endYear", enitity.EndYear);
                    cmd.Parameters.AddWithValue("@applicant", enitity.Applicant);
                    cmd.Parameters.AddWithValue("@location", enitity.Location);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
