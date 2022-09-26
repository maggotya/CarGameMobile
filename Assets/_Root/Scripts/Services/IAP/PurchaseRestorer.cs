using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    internal class PurchaseRestorer
    {
        private readonly IExtensionProvider _extensionProvider;


        public PurchaseRestorer(IExtensionProvider extensionProvider) =>
            _extensionProvider = extensionProvider;


        public void Restore()
        {
            Log("RestorePurchases started ...");

            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.OSXPlayer:
                    _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnRestoredTransactions);
                    break;

                case RuntimePlatform.Android:
                    _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnRestoredTransactions);
                    break;

                default:
                    Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
                    break;
            }
        }

        private void OnRestoredTransactions(bool result) =>
            Log("RestorePurchases continuing: " + result +
                ". If no further messages, no purchases available to restore.");

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
