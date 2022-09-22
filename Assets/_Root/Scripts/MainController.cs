using Ui;
using Profile;
using UnityEngine;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                break;
        }
    }
}