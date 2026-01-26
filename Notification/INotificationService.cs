namespace design_pattern_case_1.Notification
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Notification notification);
    }
}