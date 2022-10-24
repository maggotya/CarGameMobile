using Tool.PushNotifications.Settings;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

namespace Tool.PushNotifications
{
    internal class IOSNotificationScheduler : INotificationScheduler
    {
        public void ScheduleNotification(NotificationData notificationData)
        {
#if UNITY_IOS
            iOSNotification iosNotification = new
            {
                Identifier = notificationData.Id,
                Title = notificationData.Title,
                Body = notificationData.Text,
                Trigger = CreateIosTrigger(notificationData)
            };

            iOSNotificationCenter.ScheduleNotification(iosNotification);
#endif
        }

#if UNITY_IOS
        private iOSNotificationTrigger CreateIosTrigger(NotificationData notificationData) =>
            notificationData.RepeatType switch
            {
                NotificationRepeat.Once => new iOSNotificationCalendarTrigger()
                {
                    Year = notificationData.FireTime.Year,
                    Month = notificationData.FireTime.Month,
                    Day = notificationData.FireTime.Day,
                    Hour = notificationData.FireTime.Hour,
                    Minute = notificationData.FireTime.Minute
                },

                NotificationRepeat.Repeatable => new iOSNotificationTimeIntervalTrigger()
                {
                    Repeats = true,
                    TimeInterval = notificationData.RepeatInterval
                },

                _ => default
            };
#endif
    }
}
