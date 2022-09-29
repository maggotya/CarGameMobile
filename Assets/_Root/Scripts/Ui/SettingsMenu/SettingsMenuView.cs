using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction backToMenu) =>
            _buttonBack.onClick.AddListener(backToMenu);

        public void OnDestroy() =>
            _buttonBack.onClick.RemoveAllListeners();
    }
}