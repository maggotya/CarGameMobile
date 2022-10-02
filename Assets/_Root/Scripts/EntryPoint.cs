using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private float _speedCar;
    [SerializeField] private GameState _initialState;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(_speedCar, _initialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}