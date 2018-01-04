using Launchpad.Models.Domain;
using Launchpad.Services.Cryptography;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Launchpad.Services
{
    public class UserService
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        CryptographyService cryptsvc = new CryptographyService();
        int RAND_LENGTH = 15;
        int HASH_ITERATION_COUNT = 1;

        public int Create(NewUser model)
        {
            // return to later if I have time to check for already registered emails
            //LoginUser loginModel = GetSalt(userModel.Email);
            //if (loginModel == null)

            int id = 0;
            string salt;
            string passwordHash;

            string password = model.BasicPass;

            salt = cryptsvc.GenerateRandomString(RAND_LENGTH);
            passwordHash = cryptsvc.Hash(password, salt, HASH_ITERATION_COUNT);
            model.Salt = salt;
            model.EncryptedPass = passwordHash;

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Salt", model.Salt);
                    cmd.Parameters.AddWithValue("@HashPassword", model.EncryptedPass);

                    SqlParameter param = new SqlParameter("@Id", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return id;
        }
    }
}
