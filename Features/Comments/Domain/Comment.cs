using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using design_pattern_case_1.Features.Comments.Enum;
using design_pattern_case_1.Features.Posts.Domain;
using design_pattern_case_1.Features.Users.Domain;

namespace design_pattern_case_1.Features.Comments.Domain
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string CommentText { get; set; } = string.Empty;

        public CommentState CommentState { get; set; } = CommentState.Pending;

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
