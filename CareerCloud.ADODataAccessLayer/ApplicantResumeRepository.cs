using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository :  DBOperations,IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Applicant_Resumes(Id,Applicant,Resume,Last_Updated) " +
                        "VALUES(@id,@applicant,@resume,@lastUpdated)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@applicant", enitity.Applicant);
                    cmd.Parameters.AddWithValue("@resume", enitity.Resume);
                    cmd.Parameters.AddWithValue("@lastUpdated", enitity.LastUpdated);
                    
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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            var app_resume = new List<ApplicantResumePoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Applicant_Resumes", conn).ExecuteReader();


                while (rdr.Read())
                {
                    app_resume.Add(new ApplicantResumePoco()
                    {
                        Id = (Guid)rdr["Id"],
                        Applicant = (Guid)rdr["Applicant"],
                        Resume = (string)rdr["Resume"],
                        LastUpdated = rdr.IsDBNull(rdr.GetOrdinal("Last_Updated")) ? (DateTime?)null : (DateTime)rdr["Last_Updated"]
                    });
                }
                conn.Close();

                if (app_resume == null)
                {
                    app_resume = new List<ApplicantResumePoco>();
                }

                return app_resume;
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Applicant_Resumes WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Applicant_Resumes SET Applicant = @applicant,Resume = @resume, Last_Updated = @lastUpdated WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@applicant", enitity.Applicant);
                    cmd.Parameters.AddWithValue("@resume", enitity.Resume);
                    cmd.Parameters.AddWithValue("@lastUpdated", enitity.LastUpdated);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
