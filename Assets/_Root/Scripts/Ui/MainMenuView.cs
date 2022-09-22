using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;


        public void Init(UnityAction startGame) =>
            _buttonStart.onClick.AddListener(startGame);

        public void OnDestroy() =>
            _buttonStart.onClick.RemoveAllListeners();
    }
}