using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.Features.Posts.DTOs
{
    public class CreatePostDto
    {
        [Required]
        [MaxLength(200)]
        public string PostTitle { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
    }
}
