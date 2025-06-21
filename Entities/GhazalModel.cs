using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class GhazalModel
    {

        public string GhazalId { get; set; } = string.Empty;

        public string PoetId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ghazal title is required.")]
        [StringLength(100, ErrorMessage = "Title must be less than 100 characters.")]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        public string GhazalJson { get; set; } = string.Empty;

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "At least one couplet is required.")]
        [MinLength(1, ErrorMessage = "At least one couplet must be added.")]
        public List<CoupletsModel> Couplets { get; set; } = new List<CoupletsModel>();

        public string PoetNickname { get; set; } = string.Empty;
        public string? PoetryStyle { get; set; }
        public string? Biography { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
