using Tool.PushNotifications.Settings;

namespace Tool.PushNotifications
{
    internal interface INotificationScheduler
    {
        void ScheduleNotification(NotificationData notificationData);
    }
}
