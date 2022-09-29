using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Features.AbilitySystem.Abilities
{
    internal interface IAbilityButtonView
    {
        void Init(Sprite icon, UnityAction click);
        void Deinit();
    }

    internal class AbilityButtonView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;


        private void OnDestroy() => Deinit();


        public void Init(Sprite icon, UnityAction click)
        {
            _icon.sprite = icon;
            _button.onClick.AddListener(click);
        }

        public void Deinit()
        {
            _icon.sprite = null;
            _button.onClick.RemoveAllListeners();
        }
    }
}
