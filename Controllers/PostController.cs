using design_pattern_case_1.Data;
using design_pattern_case_1.DTO;
using design_pattern_case_1.Entity;
using design_pattern_case_1.ThirdParty.As_Sunnah;
using Enyim.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace design_pattern_case_1.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMemcachedClient memcachedClient;
        private readonly IHadithService _hadithService;

        public PostController(AppDbContext applicationDbContext, IMemcachedClient memcachedClient, IHadithService hadithService)
        {
            this.appDbContext = applicationDbContext;
            this.memcachedClient = memcachedClient;
            this._hadithService = hadithService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] CreatePostDto post)
        {
            var data = new Post()
            {
                PostTitle = post.PostTitle,
                UserId = post.UserId,
            };
            await appDbContext.Posts.AddAsync(data);
            await appDbContext.SaveChangesAsync();
            return Ok(post);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var cache = await this.memcachedClient.GetAsync<Post>($"blog-post-{id}");

            if (!cache.HasValue)
            {
                var post = await appDbContext.Posts.FirstOrDefaultAsync(x => x.PostId == id);
                await this.memcachedClient.SetAsync($"blog-post-{id}", post, new TimeSpan(2, 0, 0));
                return Ok(post);
            }

            return Ok(cache.Value);
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment(int id, [FromBody] CreateCommentDto comment)
        {
            var data = new Comment()
            {
                CommentText = comment.CommentText,
                PostId = id,
                UserId = comment.UserId,
            };
            await appDbContext.Comments.AddAsync(data);
            await appDbContext.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpPost("{postId}/vote")]
        public async Task<IActionResult> VotePost(int postId, [FromBody] VotePostDto voteDto)
        {
            // Validate post exists
            var post = await appDbContext.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
                return NotFound(new { message = "Post not found" });

            // Validate voter exists
            var voter = await appDbContext.Users.FindAsync(voteDto.VotedByUserId);
            if (voter == null)
                return NotFound(new { message = "Voter not found" });

            // Check if user already voted
            var existingVote = await appDbContext.PostVotes
                .FirstOrDefaultAsync(pv => pv.PostId == postId && pv.VotedByUserId == voteDto.VotedByUserId);

            if (existingVote != null)
                return BadRequest(new { message = "User has already voted for this post" });

            // Determine points based on voter role
            int points = voter.IsAdmin ? 5 : 1;

            // Create vote record
            var vote = new PostVote
            {
                PostId = postId,
                VotedByUserId = voteDto.VotedByUserId,
                IsAdmin = voter.IsAdmin,
                Points = points,
                VotedAt = DateTime.UtcNow
            };

            await appDbContext.PostVotes.AddAsync(vote);

            // Update post author's reputation
            post.User.Reputation += points;

            await appDbContext.SaveChangesAsync();

            return Ok(new
            {
                message = "Vote recorded successfully",
                pointsAdded = points,
                newReputation = post.User.Reputation
            });
        }

        // hadith service 
        /// <summary>
        /// Retrieves a random Hadith.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetHadith()
        {
            var hadith = await _hadithService.GetHadith();
            return Ok(hadith);
        }
    }
}
