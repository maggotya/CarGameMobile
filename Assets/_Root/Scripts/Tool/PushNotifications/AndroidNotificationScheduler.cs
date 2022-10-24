using System;
using Tool.PushNotifications.Settings;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

namespace Tool.PushNotifications
{
    internal class AndroidNotificationScheduler : INotificationScheduler
    {
        public void RegisterChannel(ChannelSettings channelSettings)
        {
#if UNITY_ANDROID
            AndroidNotificationChannel androidNotificationChannel = new
            (
                channelSettings.Id,
                channelSettings.Name,
                channelSettings.Description,
                channelSettings.Importance
            );

            AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);
#endif
        }

        public void ScheduleNotification(NotificationData notificationData)
        {
#if UNITY_ANDROID
            AndroidNotification androidNotification = CreateAndroidNotification(notificationData);
            AndroidNotificationCenter.SendNotification(androidNotification, notificationData.Id);
#endif
        }

#if UNITY_ANDROID
        private AndroidNotification CreateAndroidNotification(NotificationData notificationData) =>
            notificationData.RepeatType switch
            {
                NotificationRepeat.Once => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    notificationData.FireTime
                ),

                NotificationRepeat.Repeatable => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    notificationData.FireTime,
                    notificationData.RepeatInterval
                ),

                _ => throw new ArgumentOutOfRangeException(nameof(notificationData.RepeatType))
            };
#endif
    }
}
