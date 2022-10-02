using Ui;
using Game;
using Profile;
using UnityEngine;
using Features.Shed;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private ShedController _shedController;
    private GameController _gameController;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        DisposeControllers();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        DisposeControllers();

        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedController = new ShedController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeControllers()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _shedController?.Dispose();
        _gameController?.Dispose();
    }
}