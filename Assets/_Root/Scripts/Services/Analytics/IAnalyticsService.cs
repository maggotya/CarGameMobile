using System.Collections.Generic;

namespace Services.Analytics
{
    internal interface IAnalyticsService
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, Dictionary<string, object> eventData);
        void SendTransaction(string productId, decimal amount, string currency);
    }
}
