namespace design_pattern_case_1.Notification
{
    public class NotificationDirector
    {
        private readonly NotificationBuilder _notificationBuilder;
        public NotificationDirector(NotificationBuilder notificationBuilder)
        {
            _notificationBuilder = notificationBuilder;
        }
        public Notification BuildNotification(string recipient, string message)
        {
            return _notificationBuilder.SetNotification(recipient, message).Build();
        }
    }
}
