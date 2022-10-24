using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal class GameMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonPause;


        public void Init(UnityAction back, UnityAction pause)
        {
            _buttonBack.onClick.AddListener(back);
            _buttonPause.onClick.AddListener(pause);
        }

        private void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
            _buttonPause.onClick.RemoveAllListeners();
        }
    }
}
