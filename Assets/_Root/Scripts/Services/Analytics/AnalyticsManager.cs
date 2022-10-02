using UnityEngine;
using Services.Analytics.UnityAnalytics;

namespace Services.Analytics
{
    internal interface IAnalyticsManager
    {
        void SendGameStarted();
        void SendTransaction(string productId, decimal amount, string currency);
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

        public void SendTransaction(string productId, decimal amount, string currency)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendTransaction(productId, amount, currency);

            Log($"Sent transaction {productId}");
        }


        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);

            Log($"Sent {eventName}");
        }


        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
