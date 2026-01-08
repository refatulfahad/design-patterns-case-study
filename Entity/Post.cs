using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace design_pattern_case_1.Entity
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
    
        [Required]
        [MaxLength(200)]
        public string PostTitle { get; set; } = string.Empty;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Navigation property
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
