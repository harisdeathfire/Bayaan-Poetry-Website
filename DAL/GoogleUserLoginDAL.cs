using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DAL
{
    public class GoogleUserLoginDAL
    {
        public async Task SaveUserLogin(GoogleUserLoginModel userLogin)
        {
            using (var conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("SP_SaveUserLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 255)).Value = userLogin.userEmail;
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 255)).Value = userLogin.userName;
                    cmd.Parameters.Add(new SqlParameter("@Role", SqlDbType.NVarChar, 50)).Value = userLogin.userRole ?? "User";

                    cmd.Parameters.Add(new SqlParameter("@Profile_Id", SqlDbType.NVarChar, 8)).Value = (object)userLogin.Profile_Id ?? DBNull.Value;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }



        public async Task<GoogleUserLoginModel?> GetUserLoginByEmail(string email)
        {
            using (var conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("SP_GetUserLoginByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 255)).Value = email;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new GoogleUserLoginModel
                            {
                                userId = Convert.ToInt32(reader["UserID"]),
                                userName = reader["Username"].ToString() ?? string.Empty,
                                userEmail = reader["Email"].ToString() ?? string.Empty,
                                userRole = reader["Role"].ToString() ?? "User",
                                Profile_Id = reader["Profile_Id"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

    }
}
