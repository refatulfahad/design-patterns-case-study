namespace design_pattern_case_1.Infrastructure.Notifications
{
    public class NotificationBuilder
    {
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;

        public NotificationBuilder(string subject)
        {
            Subject = subject;
        }

        public NotificationBuilder SetNotification(string recipient, string message)
        {
            this.Recipient = recipient;
            this.Message = message;
            return this;
        }

        public Notification Build()
        {
            return new Notification(this);
        }
    }
}
