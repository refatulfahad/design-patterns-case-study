using design_pattern_case_1.Features.Users.Domain;
using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.Features.Posts.Domain
{
    public class PostVote
    {
        [Key]
        public int PostVoteId { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        public int VotedByUserId { get; set; }
        public User VotedBy { get; set; } = null!;

        public bool IsAdmin { get; set; } // Track if vote was by admin

        public int Points { get; set; } // 5 for admin, 1 for user

        public DateTime VotedAt { get; set; } = DateTime.UtcNow;
    }
}
