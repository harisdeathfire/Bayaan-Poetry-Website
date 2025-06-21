using DAL;
using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class TestimonialDAL
{
    public async Task<List<TestimonialModel>> GetAllAsync()
    {
        var testimonials = new List<TestimonialModel>();

        using (SqlConnection conn = DBHelper.GetConnection())
        {
            await conn.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("SP_GetAllTestimonials", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        testimonials.Add(new TestimonialModel
                        {
                            Id = reader.GetInt32(0),
                            Author = reader.GetString(1),
                            Message = reader.GetString(2),
                            CreatedAt = reader.GetDateTime(3),
                            IsApproved = reader.GetBoolean(4),
                            AuthorImageUrl = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Title = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Rating = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                            AuthorRole = reader.IsDBNull(8) ? null : reader.GetString(8),
                            CountryCode = reader.IsDBNull(9) ? null : reader.GetString(9)
                        });
                    }
                }
            }
        }

        return testimonials;
    }

    public async Task AddAsync(TestimonialModel testimonial)
    {
        using (SqlConnection conn = DBHelper.GetConnection())
        {
            await conn.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("SP_AddTestimonial", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Author", testimonial.Author);
                cmd.Parameters.AddWithValue("@Message", testimonial.Message);
                cmd.Parameters.AddWithValue("@AuthorImageUrl", (object?)testimonial.AuthorImageUrl ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Title", (object?)testimonial.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Rating", (object?)testimonial.Rating ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AuthorRole", (object?)testimonial.AuthorRole ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CountryCode", (object?)testimonial.CountryCode ?? DBNull.Value);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task ApproveTestimonialAsync(int id)
    {
        using var conn = DBHelper.GetConnection();
        await conn.OpenAsync();

        using var cmd = new SqlCommand("SP_ApproveTestimonial", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Id", id);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task RejectTestimonialAsync(int id)
    {
        using var conn = DBHelper.GetConnection();
        await conn.OpenAsync();

        using var cmd = new SqlCommand("SP_RejectTestimonial", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Id", id);

        await cmd.ExecuteNonQueryAsync();
    }
}
