using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class UserModel
    {
        public string? UId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name must be less than 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name must be less than 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Nick name is required")]
        [StringLength(30, ErrorMessage = "Nick name must be less than 30 characters")]
        public string? NickName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNo { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }

        public string? UserLocation { get; set; }

        public string? ProfileImageUrl { get; set; }

        [StringLength(500, ErrorMessage = "Short description must be under 500 characters")]
        public string? ShortDescription { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}
