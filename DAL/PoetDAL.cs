using DAL;
using Entities;
using Microsoft.Data.SqlClient;
using System.Data;

public class PoetDAL
{
    // Insert a new Poet
    public async Task Insert(PoetModel poet)
    {
        try
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SP_InsertPoet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProfileId", poet.UId);
                    cmd.Parameters.AddWithValue("@NickName", poet.NickName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PoetryStyle", poet.PoetryStyle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Biography", poet.Biography ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsApproved", poet.IsApproved);
                    cmd.Parameters.AddWithValue("@BecamePoetAt", poet.BecamePoetAt ?? (object)DBNull.Value);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    // Retrieve Poet by Email
    public async Task<PoetModel?> GetByEmail(string email)
    {
        PoetModel? poet = null;

        try
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SP_GetPoetByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            poet = new PoetModel
                            {
                                UId = reader["UId"].ToString(),
                                NickName = reader["NickName"].ToString(),
                                PoetryStyle = reader["PoetryStyle"].ToString(),
                                Biography = reader["Biography"].ToString(),
                                IsApproved = (bool)reader["IsApproved"],
                                BecamePoetAt = reader["BecamePoetAt"] == DBNull.Value ? null : (DateTime?)reader["BecamePoetAt"]
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return poet;
    }

    // Update Poet
    public async Task Update(PoetModel poet)
    {
        try
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SP_UpdatePoet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UId", poet.UId);
                    cmd.Parameters.AddWithValue("@NickName", poet.NickName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PoetryStyle", poet.PoetryStyle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Biography", poet.Biography ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsApproved", poet.IsApproved);
                    cmd.Parameters.AddWithValue("@BecamePoetAt", poet.BecamePoetAt ?? (object)DBNull.Value);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    // Update user role (e.g., to Poet)
    public async Task UpdateUserRole(string uId)
    {
        try
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SP_UpdateUserRole", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UId", uId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task UpdateUserRoleToUser(string uId)
    {
        try
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SP_UpdateUserRoleToUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UId", uId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }


    // Get all poets
    public async Task<List<PoetModel>> GetAllPoetsAsync()
    {
        var poets = new List<PoetModel>();

        try
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SP_GetAllPoetsWithProfileImage", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            poets.Add(new PoetModel
                            {
                                UId = reader["UId"].ToString(),
                                NickName = reader["NickName"].ToString(),
                                PoetryStyle = reader["PoetryStyle"].ToString(),
                                Biography = reader["Biography"].ToString(),
                                IsApproved = Convert.ToBoolean(reader["IsApproved"]),
                                BecamePoetAt = reader["BecamePoetAt"] == DBNull.Value ? null : (DateTime?)reader["BecamePoetAt"],
                                Email = reader["Email"].ToString(),
                                ProfileImageUrl = reader["ProfileImageUrl"] == DBNull.Value ? null : reader["ProfileImageUrl"].ToString()
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return poets;
    }

    public async Task ApprovePoetAsync(string email = null)
    {
        try
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("ApprovePoet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ApprovePoetAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<List<PoetModel>> GetUnapprovedPoetsAsync()
    {
        var poets = new List<PoetModel>();

        try
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_GetUnapprovedPoetsWithProfileImage", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            poets.Add(new PoetModel
                            {
                                UId = reader["UId"].ToString(),
                                NickName = reader["NickName"].ToString(),
                                PoetryStyle = reader["PoetryStyle"].ToString(),
                                Biography = reader["Biography"].ToString(),
                                IsApproved = Convert.ToBoolean(reader["IsApproved"]),
                                BecamePoetAt = reader["BecamePoetAt"] == DBNull.Value ? null : (DateTime?)reader["BecamePoetAt"],
                                Email = reader["Email"].ToString(),
                                ProfileImageUrl = reader["ProfileImageUrl"] == DBNull.Value ? null : reader["ProfileImageUrl"].ToString()
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return poets;
    }
    public async Task<string> GetPoetIdByEmail(string email)
    {
        using (SqlConnection conn = DBHelper.GetConnection())
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("SP_GetPoetIdByEmail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);

                var result = await cmd.ExecuteScalarAsync();
                return result?.ToString();
            }
        }
    }

}
