using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.DTO
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
