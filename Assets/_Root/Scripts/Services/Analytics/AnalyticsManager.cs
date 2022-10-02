using UnityEngine;
using Services.Analytics.UnityAnalytics;

namespace Services.Analytics
{
    internal interface IAnalyticsManager
    {
        void SendGameStarted();
    }

    internal class AnalyticsManager : MonoBehaviour, IAnalyticsManager
    {
        private IAnalyticsService[] _services;


        private void Awake() =>
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };

        public void SendGameStarted() =>
            SendEvent("Game Started");


        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }
    }
}
