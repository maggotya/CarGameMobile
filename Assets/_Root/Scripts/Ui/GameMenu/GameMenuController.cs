using Tool;
using Profile;
using UnityEngine;

namespace Ui
{
    internal class GameMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new("Prefabs/Game/GameMenu");
        private readonly ProfilePlayer _profilePlayer;


        public GameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;

            GameMenuView view = LoadView(placeForUi);
            view.Init(Back);
        }


        private GameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameMenuView>();
        }

        private void Back() => _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
