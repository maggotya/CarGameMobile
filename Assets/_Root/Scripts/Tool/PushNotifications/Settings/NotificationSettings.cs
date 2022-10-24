using System;
using UnityEngine;

namespace Tool.PushNotifications.Settings
{
    [CreateAssetMenu(fileName = nameof(NotificationSettings),
        menuName = "Settings/Notifications/" + nameof(NotificationSettings))]
    internal class NotificationSettings : ScriptableObject
    {
        [field: SerializeField] public ChannelSettings[] Channels { get; private set; }
        [field: SerializeField] public NotificationData[] Notifications { get; private set; }
    }

    [Serializable]
    internal class ChannelSettings
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [SerializeField] private Importance _importance;

    // небольшой хак, чтобы можно было использовать в других классах
    // без оборачиваиния в UNITY_ANDROID там
#if UNITY_ANDROID
        public Unity.Notifications.Android.Importance Importance =>
            (Unity.Notifications.Android.Importance)_importance;
#else
        public Importance Importance => _importance;
#endif
    }

    [Serializable]
    internal class NotificationData
    {
        // В случае с Android должен совпадать с Id канала,
        // т.к. нотификации идентифицируются и планируются именно каналами
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public NotificationRepeat RepeatType { get; private set; }
        [field: SerializeField] public Date FireTime { get; private set; }
        [field: SerializeField] public Span RepeatInterval { get; private set; }

        public override string ToString() => $"{Id}: {Title}.{Text}. {RepeatType:F}, {FireTime}, {RepeatInterval}";
    }

    internal enum Importance
    {
        None = 0,
        Low = 2,
        Default = 3,
        High = 4,
    }

    internal enum NotificationRepeat
    {
        Once,
        Repeatable
    }

    // возможность задавать в инспекторе данные для DateTime
    [Serializable]
    internal class Date
    {
        [field: SerializeField, Min(1)] public int Year { get; private set; }
        [field: SerializeField, Range(1, 12)] public int Month { get; private set; }
        [field: SerializeField, Range(1, 31)] public int Day { get; private set; }
        [field: SerializeField, Range(0, 23)] public int Hour { get; private set; }
        [field: SerializeField, Range(0, 59)] public int Minute { get; private set; }

        public static implicit operator DateTime(Date date) => new DateTime(
            date.Year, date.Month, date.Day,
            date.Hour, date.Minute, default);
    }

    // возможность задавать в инспекторе данные для TimeSpan
    [Serializable]
    internal class Span
    {
        [field: SerializeField, Min(0)] public int Seconds { get; private set; }

        public override string ToString() => Seconds.ToString();

        public static implicit operator TimeSpan(Span span) => TimeSpan.FromSeconds(span.Seconds);
    }
}
