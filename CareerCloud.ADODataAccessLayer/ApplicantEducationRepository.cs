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
    public class ApplicantEducationRepository : DBOperations, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Applicant_Educations(Id,Applicant,Major,Certificate_Diploma,Start_Date,Completion_Date,Completion_Percent) " +
                        "VALUES(@id,@applicant,@major,@certificateDiploma,@startDate,@completionDate,@completionPercent)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@applicant", enitity.Applicant);
                    cmd.Parameters.AddWithValue("@major", enitity.Major);
                    cmd.Parameters.AddWithValue("@certificateDiploma", enitity.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@startDate", enitity.StartDate);
                    cmd.Parameters.AddWithValue("@completionDate", enitity.CompletionDate);
                    cmd.Parameters.AddWithValue("@completionPercent", enitity.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            var app_education = new List<ApplicantEducationPoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Applicant_Educations", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_education.Add(new ApplicantEducationPoco()
                    {
                        Id = (Guid)rdr["Id"],
                        CertificateDiploma = (string)rdr["Certificate_Diploma"],
                        StartDate = (DateTime)rdr["Start_Date"],
                        CompletionDate = (DateTime)rdr["Completion_Date"],
                        CompletionPercent = (byte)rdr["Completion_Percent"],
                        TimeStamp = (byte[])rdr["Time_Stamp"],
                        Applicant = (Guid)rdr["Applicant"],
                        Major = (string)rdr["Major"],


                    });
                }

                conn.Close();

                if (app_education == null)
                {
                    app_education = new List<ApplicantEducationPoco>();
                }

                return app_education;
            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Applicant_Educations WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Applicant_Educations SET Certificate_Diploma = @certificateDiploma,Start_Date = @startDate, Completion_Date = @CompletionDate,Completion_Percent = @CompletionPercent,Applicant = @Applicant,Major = @Major WHERE ID = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@applicant", enitity.Applicant);
                    cmd.Parameters.AddWithValue("@major", enitity.Major);
                    cmd.Parameters.AddWithValue("@certificateDiploma", enitity.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@startDate", enitity.StartDate);
                    cmd.Parameters.AddWithValue("@completionDate", enitity.CompletionDate);
                    cmd.Parameters.AddWithValue("@completionPercent", enitity.CompletionPercent);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
