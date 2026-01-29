using design_pattern_case_1.Data;
using design_pattern_case_1.Entity;
using design_pattern_case_1.Enum;
using Microsoft.EntityFrameworkCore;

namespace design_pattern_case_1.Observer
{
    /// <summary>
    /// Concrete Observer: Manages user bans based on moderation actions
    /// </summary>
    public class UserBanObserver : ICommentObserver
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public UserBanObserver(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task OnCommentDisabledAsync(Comment comment, string reason)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Count how many comments this user has had disabled
            var disabledCount = await dbContext.Comments
                .Where(c => c.UserId == comment.UserId && c.CommentState == CommentState.Disable)
                .CountAsync();

            Console.WriteLine($"[UserBan] User {comment.User.UserName} has {disabledCount} disabled comments");

            // Ban user if they have 3 or more disabled comments
            if (disabledCount >= 3)
            {
                var user = await dbContext.Users.FindAsync(comment.UserId);
                if (user != null && user.Status != UserStatus.Banned)
                {
                    user.Status = UserStatus.Banned;
                    user.ViolationCount = disabledCount;
                    await dbContext.SaveChangesAsync();
                    
                    Console.WriteLine($"[UserBan] User {comment.User.UserName} has been BANNED (3+ violations)");
                }
            }
        }
    }
}
