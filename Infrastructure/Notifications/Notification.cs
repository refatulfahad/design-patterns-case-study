namespace design_pattern_case_1.Infrastructure.Notifications
{
    public class Notification
    {
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;

        public Notification(NotificationBuilder notification)
        {
            this.Subject = notification.Subject;
            this.Message = notification.Message;
            this.Recipient = notification.Recipient;
        }
    }
}
