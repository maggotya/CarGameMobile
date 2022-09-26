using Profile;
using UnityEngine;
using Services.Ads;
using Services.Analytics;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsTools _adsService;
    [SerializeField] private AnalyticsManager _analytics;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        _analytics.SendMainMenuOpened();
        _adsService.ShowInterstitial();
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}