using Entities;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DAL
{
    public class GhazalDAL
    {
        public async Task InsertGhazal(GhazalModel ghazal)
        {
            Console.WriteLine($"DEBUG in DAL: Title = {ghazal.Title}");

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_InsertGhazal", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GhazalId", ghazal.GhazalId);
                    cmd.Parameters.AddWithValue("@PoetId", ghazal.PoetId);
                    cmd.Parameters.AddWithValue("@Title", ghazal.Title ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhazalJson", ghazal.GhazalJson ?? (object)DBNull.Value);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<GhazalModel> GetGhazalById(string ghazalId)
        {
            GhazalModel ghazal = null;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_GetGhazalById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GhazalId", ghazalId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            ghazal = new GhazalModel
                            {
                                GhazalId = reader.GetString(reader.GetOrdinal("GhazalId")),
                                PoetId = reader.GetString(reader.GetOrdinal("PoetId")),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString(reader.GetOrdinal("Title")),
                                GhazalJson = reader.IsDBNull(reader.GetOrdinal("GhazalJson")) ? null : reader.GetString(reader.GetOrdinal("GhazalJson")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            };
                        }
                    }
                }
            }

            return ghazal;
        }

        public async Task<List<GhazalModel>> GetAllGhazals()
        {
            List<GhazalModel> ghazals = new List<GhazalModel>();

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_GetAllGhazals", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var ghazal = new GhazalModel
                        {
                            GhazalId = reader.GetString(reader.GetOrdinal("GhazalId")),
                            PoetId = reader.IsDBNull(reader.GetOrdinal("PoetId")) ? string.Empty : reader.GetString(reader.GetOrdinal("PoetId")),
                            Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? string.Empty : reader.GetString(reader.GetOrdinal("Title")),
                            GhazalJson = reader.IsDBNull(reader.GetOrdinal("GhazalJson")) ? string.Empty : reader.GetString(reader.GetOrdinal("GhazalJson")),
                            CreatedAt = reader.IsDBNull(reader.GetOrdinal("CreatedAt")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            PoetNickname = reader.IsDBNull(reader.GetOrdinal("PoetNickname")) ? string.Empty : reader.GetString(reader.GetOrdinal("PoetNickname"))
                        };

                        ghazals.Add(ghazal);
                    }
                }
            }

            return ghazals;
        }

        public async Task<bool> DeleteGhazal(string ghazalId)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_DeleteGhazal", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GhazalId", ghazalId);

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<GhazalModel>> GetGhazalsByPoetIdOrNickname(string poetId = null, string nickname = null)
        {
            var ghazals = new List<GhazalModel>();

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_GetGhazalsByPoetId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PoetId", string.IsNullOrWhiteSpace(poetId) ? DBNull.Value : (object)poetId);
                    cmd.Parameters.AddWithValue("@NickName", string.IsNullOrWhiteSpace(nickname) ? DBNull.Value : (object)nickname);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var ghazal = new GhazalModel
                            {
                                GhazalId = reader["GhazalId"].ToString(),
                                PoetId = reader["PoetId"].ToString(),
                                Title = reader["Title"]?.ToString(),
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                GhazalJson = reader["GhazalJson"]?.ToString()
                            };
                            ghazals.Add(ghazal);
                        }
                    }
                }
            }

            return ghazals;
        }

        public async Task<List<GhazalModel>> GetGhazalsByEmail(string email)
        {
            var ghazals = new List<GhazalModel>();

            using (var conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("SP_GetGhazalsByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var ghazal = new GhazalModel
                            {
                                GhazalId = reader["GhazalId"].ToString(),
                                PoetId = reader["PoetId"].ToString(),
                                Title = reader["Title"].ToString(),
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                GhazalJson = reader["GhazalJson"].ToString()
                            };

                            ghazals.Add(ghazal);
                        }
                    }
                }
            }

            return ghazals;
        }

        public async Task<List<GhazalModel>> GetGhazalsByNicknameAsync(string nickname)
        {
            var ghazals = new List<GhazalModel>();

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SP_GetGhazalsByNickname", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nickname", nickname);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var ghazal = new GhazalModel
                            {
                                GhazalId = reader["GhazalId"].ToString(),
                                PoetId = reader["PoetId"].ToString(),
                                Title = reader["Title"].ToString(),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                GhazalJson = reader["GhazalJson"].ToString(),
                                PoetNickname = reader["NickName"]?.ToString(),
                                PoetryStyle = reader["PoetryStyle"]?.ToString(),
                                Biography = reader["Biography"]?.ToString(),
                                ProfileImageUrl = reader["ProfileImageUrl"]?.ToString()
                            };

                            ghazals.Add(ghazal);
                        }
                    }
                }
            }

            return ghazals;
        }
    }
}
