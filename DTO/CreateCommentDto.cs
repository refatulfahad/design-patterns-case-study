using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.DTO
{
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(1000)]
        public string CommentText { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
    }
}
