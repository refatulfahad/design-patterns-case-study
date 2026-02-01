namespace design_pattern_case_1.Infrastructure.Notifications
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Notification notification);
    }
}