using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal class GameMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction back) =>
            _buttonBack.onClick.AddListener(back);

        private void OnDestroy() =>
            _buttonBack.onClick.RemoveAllListeners();
    }
}
