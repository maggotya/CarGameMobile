using Ui;
using Game;
using Profile;
using UnityEngine;
using Features.Shed;
using Features.Fight;
using Features.Rewards;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private RewardController _rewardController;
    private StartFightController _startFightController;
    private FightController _fightController;
    private GameController _gameController;

    private ShedContext _shedContext;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        DisposeChildObjects();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        DisposeChildObjects();

        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedContext = new ShedContext(_placeForUi, _profilePlayer);
                break;
            case GameState.DailyReward:
                _rewardController = new RewardController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                _startFightController = new StartFightController(_placeForUi, _profilePlayer);
                break;
            case GameState.Fight:
                _fightController = new FightController(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeChildObjects()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _rewardController?.Dispose();
        _startFightController?.Dispose();
        _fightController?.Dispose();
        _gameController?.Dispose();

        _shedContext?.Dispose();
    }
}