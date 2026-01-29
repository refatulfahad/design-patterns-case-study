using design_pattern_case_1.Entity;
using design_pattern_case_1.Notification;

namespace design_pattern_case_1.Observer
{
    /// <summary>
    /// Concrete Observer: Sends email notifications when comments are moderated
    /// </summary>
    public class EmailNotificationObserver : ICommentObserver
    {
        private readonly INotificationService _notificationService;

        public EmailNotificationObserver(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task OnCommentDisabledAsync(Comment comment, string reason)
        {
            Console.WriteLine($"[EmailObserver] Sending email notification for disabled comment {comment.CommentId}");

            var notificationBuilder = new NotificationBuilder("badComment");
            var notificationDirector = new NotificationDirector(notificationBuilder);
            
            var notification = notificationDirector.BuildNotification(
                comment.User.UserName,
                $"Your comment with ID {comment.CommentId} has been disabled. " +
                $"Reason: {reason}. " +
                $"Due to our community guidelines, you have been banned."
            );

            await _notificationService.SendNotificationAsync(notification);
            Console.WriteLine($"[EmailObserver] Email sent to {comment.User.UserName}");
        }
    }
}
