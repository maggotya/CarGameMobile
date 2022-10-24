using UnityEngine;
using UnityEngine.UI;
using Tool.PushNotifications;
using Tool.PushNotifications.Settings;

namespace Tool.Notifications.Examples
{
    internal class NotificationWindow : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private NotificationSettings _settings;

        [Header("Scene Components")]
        [SerializeField] private Button _buttonNotification;

        private INotificationScheduler _scheduler;


        private void Awake()
        {
            NotificationSchedulerFactory schedulerFactory = new(_settings);
            _scheduler = schedulerFactory.Create();
        }

        private void OnEnable() =>
            _buttonNotification.onClick.AddListener(CreateNotification);

        private void OnDisable() =>
            _buttonNotification.onClick.RemoveAllListeners();


        private void CreateNotification()
        {
            foreach (NotificationData notificationData in _settings.Notifications)
                _scheduler.ScheduleNotification(notificationData);
        }
    }
}