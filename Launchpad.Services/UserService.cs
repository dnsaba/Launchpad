using Launchpad.Models.Domain;
using Launchpad.Services.Cryptography;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Web;

namespace Launchpad.Services
{
    public class UserService
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        CryptographyService cryptsvc = new CryptographyService();
        int RAND_LENGTH = 15;
        int HASH_ITERATION_COUNT = 1;

        public bool LogIn(LoginRequest model)
        {
            bool isSuccessful = false;
            LoginUser userData = GetInfo(model.Email);

            if (userData != null && !String.IsNullOrEmpty(userData.Salt))
            {
                int multOf4 = userData.Salt.Length % 4;
                if (multOf4 > 0)
                {
                    userData.Salt += new string('=', 4 - multOf4);
                }

                string passwordHash = cryptsvc.Hash(model.BasicPass, userData.Salt, HASH_ITERATION_COUNT);

                var claims = new List<Claim>();
                claims.Add(new Claim("id", userData.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, userData.Email));
                var id = new ClaimsIdentity(claims,
                                            DefaultAuthenticationTypes.ApplicationCookie);

                var ctx = HttpContext.Current.Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(id);

                if (model.Email == userData.Email && passwordHash == userData.HashPassword)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

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

        private LoginUser GetInfo(string email)
        {
            LoginUser model = null;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Users_SelectByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        model = new LoginUser();
                        int index = 0;
                        model.Id = reader.GetInt32(index++);
                        model.Email = reader.GetString(index++);
                        model.Salt = reader.GetString(index++);
                        model.HashPassword = reader.GetString(index++);
                    }

                }
                conn.Close();
            }
            return model;
        }
    }
}
