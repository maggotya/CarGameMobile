using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private InitialProfilePlayer _initialProfilePlayer;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Start()
    {
        ProfilePlayer profilePlayer = new(
            _initialProfilePlayer.Car.Speed,
            _initialProfilePlayer.Car.JumpHeight,
            _initialProfilePlayer.State
        );

        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}