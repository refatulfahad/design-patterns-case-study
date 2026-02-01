using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.Features.Posts.DTOs
{
    public class VotePostDto
    {
        [Required]
  public int PostId { get; set; }

        [Required]
        public int VotedByUserId { get; set; }
    }
}
