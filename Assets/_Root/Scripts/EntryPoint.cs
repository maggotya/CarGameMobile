using Profile;
using UnityEngine;
using UnityEngine.Analytics;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        Analytics.CustomEvent("MainMenuOpened");
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}