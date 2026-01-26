using design_pattern_case_1.Data;
using design_pattern_case_1.Entity;
using design_pattern_case_1.Enum;
using design_pattern_case_1.Notification;
using Microsoft.EntityFrameworkCore;

namespace design_pattern_case_1.Services
{
    public class CommentBackGroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public CommentBackGroundService(
            IServiceScopeFactory scopeFactory
            )
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRun = now.Date.AddHours(12); // today 12:00 PM

                if (now > nextRun)
                {
                    // If already past 12 PM, schedule for tomorrow
                    nextRun = nextRun.AddDays(1);
                }

                var delay = nextRun - now;

                await Task.Delay(delay, stoppingToken);

                await ProcessCommentsAsync();

                // Wait 24 hours for the next run
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task ProcessCommentsAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                                     .GetRequiredService<AppDbContext>();

                var comments = await dbContext.Comments
                                                .Include(c => c.User)
                                                .Where(c => c.CommentState == CommentState.Pending).ToListAsync();

                var badWordCheckerService = new BadWordCheckerService();
                var badComments = new List<Comment>();

                using var notificationScope = _scopeFactory.CreateScope();

                var notificationService = notificationScope.ServiceProvider
                    .GetRequiredService<INotificationService>();

                foreach (var comment in comments)
                {
                    if (badWordCheckerService.ContainsBadWords(comment.CommentText))
                    {
                        comment.CommentState = CommentState.Disable;
                        badComments.Add(comment);
                    }
                    else
                    {
                        comment.CommentState = CommentState.Live;
                    }
                }

                foreach (var badComment in badComments)
                {
                    var notificationBuilder = new NotificationBuilder("badComment");
                    var notificationDirector = new NotificationDirector(notificationBuilder);
                    var notification = notificationDirector.BuildNotification(
                        badComment.User.UserName,
                        $"Your comment with ID {badComment.CommentId} has been disabled due to inappropriate content. " +
                        $"Due to our community guidelines, you have been banned."
                    );

                    await notificationService.SendNotificationAsync(notification);
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
