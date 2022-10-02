using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal class UnityAdsService : MonoBehaviour, IUnityAdsInitializationListener, IAdsService
    {
        [Header("Components")]
        [SerializeField] private UnityAdsSettings _settings;

        [field: Header("Events")]
        [field: SerializeField] public UnityEvent Initialized { get; private set; }

        public IAdsPlayer InterstitialPlayer { get; private set; }
        public IAdsPlayer RewardedPlayer { get; private set; }
        public IAdsPlayer BannerPlayer { get; private set; }
        public bool IsInitialized => Advertisement.isInitialized;


        private void Awake()
        {
            InitializeAds();
            InitializePlayers();
        }

        private void InitializeAds() =>
            Advertisement.Initialize(
                _settings.GameId,
                _settings.TestMode,
                _settings.EnablePerPlacementMode,
                this);

        private void InitializePlayers()
        {
            InterstitialPlayer = CreateInterstitial();
            RewardedPlayer = CreateRewarded();
            BannerPlayer = CreateBanner();
        }


        private IAdsPlayer CreateInterstitial() =>
            _settings.Interstitial.Enabled
                ? new InterstitialPlayer(_settings.Interstitial.Id)
                : new StubPlayer("");

        private IAdsPlayer CreateRewarded() =>
            _settings.Rewarded.Enabled
                ? new RewardedAdsPlayer(_settings.Rewarded.Id)
                : new StubPlayer("");

        private IAdsPlayer CreateBanner() =>
            new StubPlayer("");


        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            Log("Initialization complete.");
            Initialized?.Invoke();
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            Error($"Initialization Failed: {error.ToString()} - {message}");


        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
