using design_pattern_case_1.Features.Comments.Domain;
using design_pattern_case_1.Features.Comments.DTOs;
using design_pattern_case_1.Features.Comments.Factories;
using design_pattern_case_1.Infrastructure.Data;
using design_pattern_case_1.Shared.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace design_pattern_case_1.Features.Comments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly ConfigService configService;

        public CommentController(
            AppDbContext applicationDbContext,
            ConfigService configService
            )
        {
            this.appDbContext = applicationDbContext;
            this.configService = configService;
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment(int id, [FromBody] CreateCommentDto comment)
        {
            if (await configService.GetConfigValue("comment_status") == "false")
            {
                return BadRequest(new { message = "Comments are disabled." });
            }

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

        [HttpGet("{id}/edit-permission")]
        public async Task<IActionResult> EditPermitted(int id)
        {
            var comment = await appDbContext.Comments
                .Where(c => c.CommentId == id)
                .FirstOrDefaultAsync();

            if (comment == null)
            {
                return NotFound(new { message = "Comment not found" });
            }

            var commentState = CommentStateFactory.CreateState(comment.CommentState);
            var result = commentState.Edit(id, appDbContext);

            return Ok(new
            {
                commentId = id,
                currentState = comment.CommentState.ToString(),
                canEdit = result.IsAllowed,
                message = result.Message
            });
        }

        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await appDbContext.Comments
                .Where(c => c.CommentId == id)
                .FirstOrDefaultAsync();

            if (comment == null)
            {
                return NotFound(new { message = "Comment not found" });
            }

            var commentState = CommentStateFactory.CreateState(comment.CommentState);
            var result = await commentState.DeleteAsync(id, appDbContext);

            if (!result.IsAllowed)
            {
                return BadRequest(new
                {
                    commentId = id,
                    currentState = comment.CommentState.ToString(),
                    success = false,
                    message = result.Message
                });
            }

            return Ok(new
            {
                commentId = id,
                success = result.OperationPerformed,
                message = result.Message
            });
        }
    }
}
