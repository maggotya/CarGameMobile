using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonGame;
        [SerializeField] private Button _buttonMenu;


        public void Init(UnityAction game, UnityAction menu)
        {
            _buttonGame.onClick.AddListener(game);
            _buttonMenu.onClick.AddListener(menu);
        }

        private void OnDestroy()
        {
            _buttonGame.onClick.RemoveAllListeners();
            _buttonMenu.onClick.RemoveAllListeners();
        }


        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
