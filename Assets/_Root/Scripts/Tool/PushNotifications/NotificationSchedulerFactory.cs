using Tool.PushNotifications.Settings;

namespace Tool.PushNotifications
{
    internal class NotificationSchedulerFactory
    {
        private readonly NotificationSettings _settings;


        public NotificationSchedulerFactory(NotificationSettings settings) =>
            _settings = settings;


        public INotificationScheduler Create()
        {
#if UNITY_EDITOR
            return new StubNotificationScheduler();
#elif UNITY_ANDROID
            var scheduler = new AndroidNotificationScheduler();
            foreach (ChannelSettings channelSettings in _settings.Channels)
                scheduler.RegisterChannel(channelSettings);

            return scheduler;
#elif UNITY_IOS
            return new IOSNotificationScheduler();
#else
            return new StubNotificationScheduler();
#endif
        }
    }
}
