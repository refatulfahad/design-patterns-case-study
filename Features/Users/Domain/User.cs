using design_pattern_case_1.Features.Comments.Domain;
using design_pattern_case_1.Features.Posts.Domain;
using design_pattern_case_1.Features.Users.Enums;
using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.Features.Users.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;
        
        public bool IsAdmin { get; set; }

        // Reputation system
        public int Reputation { get; set; } = 0;

        public UserStatus Status { get; set; } = UserStatus.Active;

        public int ViolationCount { get; set; } = 0;

        //Navigation properties
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
