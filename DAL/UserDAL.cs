using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        public async Task<List<UserModel>> GetAllProfilesAsync()
        {
            List<UserModel> userList = new List<UserModel>();

            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_GetAllProfiles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            UserModel user = new UserModel
                            {
                                UId = dr["U_id"]?.ToString(),
                                FirstName = dr["FirstName"]?.ToString(),
                                LastName = dr["LastName"]?.ToString(),
                                NickName = dr["NickName"]?.ToString(),
                                DOB = dr["DOB"] != DBNull.Value ? Convert.ToDateTime(dr["DOB"]) : DateTime.MinValue,
                                PhoneNo = dr["PhoneNo"]?.ToString(),
                                City = dr["City"]?.ToString(),
                                Country = dr["Country"]?.ToString(),
                                UserLocation = dr["UserLocation"]?.ToString(),
                                ProfileImageUrl = dr["ProfileImageUrl"]?.ToString(),
                                ShortDescription = dr["ShortDescription"]?.ToString(),
                                CreatedAt = dr["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(dr["CreatedAt"]) : DateTime.MinValue,
                                IsActive = dr["IsActive"] != DBNull.Value && Convert.ToBoolean(dr["IsActive"]),
                                Email = dr["Email"]?.ToString(),
                                Role = dr["Role"]?.ToString()
                            };
                            userList.Add(user);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.Error.WriteLine("SQL Error in GetAllProfilesAsync: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("General Error in GetAllProfilesAsync: " + ex.Message);
            }

            return userList;
        }


        public void Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Invalid user ID.", nameof(id));

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteProfile", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@U_id", SqlDbType.VarChar, 8)).Value = id.Trim();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.Error.WriteLine($"[SQL ERROR] Delete failed for ID {id}: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ERROR] Delete failed for ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task Update(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.UId))
                throw new ArgumentException("Missing UId", nameof(user.UId));

            try
            {
                // Debugging: Check if the record exists before attempting an update
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    await conn.OpenAsync();

                    // Check if the user exists
                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Profiles WHERE U_id = @U_id", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@U_id", user.UId);
                        var result = await checkCmd.ExecuteScalarAsync();

                        if (Convert.ToInt32(result) == 0)
                        {
                            Console.WriteLine($"❌ Profile with UId: {user.UId} not found.");
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"✅ Profile found for UId: {user.UId}");
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SP_UpdateProfile", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@U_id", user.UId);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NickName", user.NickName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOB", user.DOB != default ? user.DOB : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhoneNo", string.IsNullOrWhiteSpace(user.PhoneNo) ? (object)DBNull.Value : user.PhoneNo);
                        cmd.Parameters.AddWithValue("@City", user.City ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Country", user.Country ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserLocation", user.UserLocation ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProfileImageUrl", user.ProfileImageUrl ?? "default.jpg");
                        cmd.Parameters.AddWithValue("@ShortDescription", user.ShortDescription ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine($"Rows affected: {rows}");

                        if (rows > 0)
                        {
                            Console.WriteLine($" Profile updated successfully for UId: {user.UId}");
                        }
                        else
                        {
                            Console.WriteLine($" Update executed but no rows were affected. UId might not exist: {user.UId}");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.Error.WriteLine($" [SQL ERROR] Update failed for UId: {user.UId} - {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($" [ERROR] Unexpected error during profile update for UId: {user.UId} - {ex.Message}");
                throw;
            }
        }



        public async Task<UserModel> GetProfileById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Invalid user ID.", nameof(id));

            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_GetProfileById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@U_id", id);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return new UserModel
                                {
                                    UId = dr["U_id"].ToString(),
                                    FirstName = dr["FirstName"].ToString(),
                                    LastName = dr["LastName"].ToString(),
                                    NickName = dr["NickName"].ToString(),
                                    DOB = Convert.ToDateTime(dr["DOB"]),
                                    PhoneNo = dr["PhoneNo"].ToString(),
                                    City = dr["City"].ToString(),
                                    Country = dr["Country"].ToString(),
                                    UserLocation = dr["UserLocation"].ToString(),
                                    ProfileImageUrl = dr["ProfileImageUrl"].ToString(),
                                    ShortDescription = dr["ShortDescription"].ToString(),
                                    CreatedAt = Convert.ToDateTime(dr["CreatedAt"]),
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    Email = dr["Email"].ToString(),
                                    Role = dr["Role"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.Error.WriteLine($"[SQL ERROR] GetProfileById failed for ID {id}: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ERROR] GetProfileById failed for ID {id}: {ex.Message}");
            }

            return null;
        }


        public async Task<UserModel> GetProfileByEmail(string email)
        {
            UserModel profile = null;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SP_GetProfileByEmail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);

                await conn.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    profile = new UserModel
                    {
                        UId = reader["U_id"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        NickName = reader["NickName"].ToString(),
                        Email = reader["Email"].ToString(),
                        DOB = reader["DOB"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["DOB"],
                        PhoneNo = reader["PhoneNo"].ToString(),
                        City = reader["City"].ToString(),
                        Country = reader["Country"].ToString(),
                        UserLocation = reader["UserLocation"].ToString(),
                        ShortDescription = reader["ShortDescription"].ToString(),
                        IsActive = reader["IsActive"] == DBNull.Value ? false : (bool)reader["IsActive"],
                        ProfileImageUrl = reader["ProfileImageUrl"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }

                await reader.CloseAsync();
            }

            return profile;
        }



        public async Task CreateProfile(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("CreateProfile", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@U_id", user.UId);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NickName", user.NickName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOB", user.DOB == default ? (object)DBNull.Value : user.DOB);
                        cmd.Parameters.AddWithValue("@PhoneNo", string.IsNullOrWhiteSpace(user.PhoneNo) ? (object)DBNull.Value : user.PhoneNo);
                        cmd.Parameters.AddWithValue("@City", user.City ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Country", user.Country ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserLocation", user.UserLocation ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProfileImageUrl", user.ProfileImageUrl ?? "default.jpg");

                        cmd.Parameters.AddWithValue("@ShortDescription", user.ShortDescription ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);


                        int rows = await cmd.ExecuteNonQueryAsync();

                        if (rows > 0)
                        {
                            Console.WriteLine("✅ Profile successfully created.");
                        }
                        else
                        {
                            Console.WriteLine("⚡ Failed to create profile.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"❌ [SQL ERROR] Failed to create profile: {ex.Message}");

                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"❌ [ERROR] Failed to create profile: {ex.Message}");

                throw;
            }
        }

        public async Task<bool> DeleteUserProfileAsync(string uId)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_DeleteUserProfile", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UId", uId);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Optionally log error here
                        throw new Exception("Error deleting user profile: " + ex.Message, ex);
                    }
                }
            }
        }
    }
}