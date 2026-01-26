using Dapr.Client;

namespace design_pattern_case_1.Notification
{
    public class DaprNotificationService: INotificationService
    {
        private readonly DaprClient _daprClient;

        public DaprNotificationService(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            await _daprClient.PublishEventAsync("design-pattern-pub-sub", "notification-topic", notification);
            Console.WriteLine($"Dapr Notification sent: {notification.Message}");
        }
    }
}
