using Tool;
using Profile;
using UnityEngine;

namespace Ui
{
    internal class GameMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new("Prefabs/Game/GameMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly Pause _pause;


        public GameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;

            GameMenuView view = LoadView(placeForUi);
            view.Init(Back, Pause);

            _pause = new Pause();
            CreatePauseMenuController(placeForUi, profilePlayer, _pause);
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            if (_pause.IsEnabled)
                _pause.Disable();
        }


        private GameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameMenuView>();
        }

        private PauseMenuController CreatePauseMenuController(
            Transform placeForUi, ProfilePlayer profilePlayer, Pause pause)
        {
            PauseMenuController pauseMenuController = new(placeForUi, profilePlayer, pause);
            AddController(pauseMenuController);

            return pauseMenuController;
        }

        private void Pause() => _pause.Enable();
        private void Back() => _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
