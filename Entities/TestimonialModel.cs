using System;
using System.ComponentModel.DataAnnotations;
namespace Entities
{
   public class TestimonialModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Author { get; set; }

    public string Title { get; set; }

    public string AuthorRole { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public string CountryCode { get; set; }

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int? Rating { get; set; }

    public string AuthorImageUrl { get; set; }

    [Required(ErrorMessage = "Message is required")]
    public string Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsApproved { get; set; } = false;
}
}

