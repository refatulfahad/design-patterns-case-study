using design_pattern_case_1.Data;
using design_pattern_case_1.Entity;
using design_pattern_case_1.Enum;
using design_pattern_case_1.Observer;
using Microsoft.EntityFrameworkCore;

namespace design_pattern_case_1.Services
{
    public class CommentBackGroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CommentSubject _commentSubject;

        public CommentBackGroundService(
            IServiceScopeFactory scopeFactory,
            CommentSubject commentSubject)
        {
            _scopeFactory = scopeFactory;
            _commentSubject = commentSubject;
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
                                                .Where(c => c.CommentState == CommentState.Pending)
                                                .ToListAsync();

                Console.WriteLine($"[Background Service] Processing {comments.Count} pending comments");

                var badWordCheckerService = new BadWordCheckerService();

                foreach (var comment in comments)
                {
                    if (badWordCheckerService.ContainsBadWords(comment.CommentText))
                    {
                        // Disable comment
                        comment.CommentState = CommentState.Disable;
                        
                        // Notify observers (Observer Pattern)
                        await _commentSubject.NotifyCommentDisabledAsync(
                            comment, 
                            "Contains inappropriate content"
                        );
                    }
                    else
                    {
                        // Approve comment
                        comment.CommentState = CommentState.Live;
                    }
                }

                await dbContext.SaveChangesAsync();
                Console.WriteLine($"[Background Service] Completed processing comments");
            }
        }
    }
}

