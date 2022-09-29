using Tool;
using Profile;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class SettingsMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new("Prefabs/Ui/SettingsMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsMenuView _view;


        public SettingsMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(BackToMenu);
        }


        private SettingsMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsMenuView>();
        }

        private void BackToMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}