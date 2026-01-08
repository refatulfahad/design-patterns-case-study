using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.DTO
{
    public class VotePostDto
    {
        [Required]
  public int PostId { get; set; }

        [Required]
        public int VotedByUserId { get; set; }
    }
}
