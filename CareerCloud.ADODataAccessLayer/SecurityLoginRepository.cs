using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : DBOperations, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Security_Logins(Id,Created_Date,Password_Update_Date,Agreement_Accepted_Date,Is_Locked,Is_Inactive,Email_Address, Phone_Number, Full_Name,Force_Change_Password,Prefferred_Language,Login,Password) " +
                        "VALUES(@id,@created,@passwordUpdate,@agreementAccepted,@isLocked,@isInactive,@emailAddress,@phoneNumber,@fullName,@forceChangePassword,@prefferredLanguage,@login,@password)";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@created", enitity.Created);
                    cmd.Parameters.AddWithValue("@passwordUpdate", enitity.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@agreementAccepted", enitity.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@isLocked", enitity.IsLocked);
                    cmd.Parameters.AddWithValue("@isInactive", enitity.IsInactive);
                    cmd.Parameters.AddWithValue("@emailAddress", enitity.EmailAddress);
                    cmd.Parameters.AddWithValue("@phoneNumber", enitity.PhoneNumber);
                    cmd.Parameters.AddWithValue("@fullName", enitity.FullName);
                    cmd.Parameters.AddWithValue("@forceChangePassword", enitity.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@prefferredLanguage", enitity.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@login", enitity.Login);
                    cmd.Parameters.AddWithValue("@password", enitity.Password);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            var securuty_logss = new List<SecurityLoginPoco>();
            using (var conn = new SqlConnection(getConnectionString()))
            {

                conn.Open();
                SqlDataReader rdr = new SqlCommand("Select * from Security_Logins", conn).ExecuteReader();


                while (rdr.Read())
                {
                    securuty_logss.Add(new SecurityLoginPoco()
                    {
                        Id = (Guid)rdr["Id"],
                        Created = (DateTime)rdr["Created_Date"],
                        PasswordUpdate = rdr.IsDBNull(rdr.GetOrdinal("Password_Update_Date")) ? null : (DateTime?)rdr["Password_Update_Date"],
                        AgreementAccepted = rdr.IsDBNull(rdr.GetOrdinal("Agreement_Accepted_Date")) ? null : (DateTime?)rdr["Agreement_Accepted_Date"],
                        IsLocked = (bool)rdr["Is_Locked"],
                        IsInactive = (bool)rdr["Is_Inactive"],
                        EmailAddress = rdr.IsDBNull(rdr.GetOrdinal("Email_Address")) ? null : (string)rdr["Email_Address"],
                        PhoneNumber = rdr.IsDBNull(rdr.GetOrdinal("Phone_Number")) ? null : (string)rdr["Phone_Number"],
                        FullName = rdr.IsDBNull(rdr.GetOrdinal("Full_Name")) ? null : (string)rdr["Full_Name"],
                        ForceChangePassword = (bool)rdr["Force_Change_Password"],
                        PrefferredLanguage = rdr.IsDBNull(rdr.GetOrdinal("Prefferred_Language")) ? null : (string)rdr["Prefferred_Language"],
                        Login = rdr.IsDBNull(rdr.GetOrdinal("Login")) ? null : (string)rdr["Login"],
                        Password = rdr.IsDBNull(rdr.GetOrdinal("Password")) ? null : (string)rdr["Password"]
                    });
                }
                
                conn.Close();

                if (securuty_logss == null)
                {
                    securuty_logss = new List<SecurityLoginPoco>();
                }

                return securuty_logss;
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Security_Logins WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(getConnectionString()))
            {
                foreach (var enitity in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Security_Logins SET Created_Date = @created," +
                        "Password_Update_Date = @passwordUpdate, Agreement_Accepted_Date = @agreementAccepted,Is_Locked = @isLocked," +
                        "Is_Inactive = @IsInactive,Email_Address = @emailAddress ,Phone_Number = @phoneNumber,Full_Name = @fullName,Force_Change_Password = @forceChangePassword, Prefferred_Language = @prefferredLanguage, Login = @login, Password = @password WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", enitity.Id);
                    cmd.Parameters.AddWithValue("@created", enitity.Created);
                    cmd.Parameters.AddWithValue("@passwordUpdate", enitity.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@agreementAccepted", enitity.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@isLocked", enitity.IsLocked);
                    cmd.Parameters.AddWithValue("@isInactive", enitity.IsInactive);
                    cmd.Parameters.AddWithValue("@emailAddress", enitity.EmailAddress);
                    cmd.Parameters.AddWithValue("@phoneNumber", enitity.PhoneNumber);
                    cmd.Parameters.AddWithValue("@fullName", enitity.FullName);
                    cmd.Parameters.AddWithValue("@forceChangePassword", enitity.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@prefferredLanguage", enitity.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@login", enitity.Login);
                    cmd.Parameters.AddWithValue("@password", enitity.Password);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
