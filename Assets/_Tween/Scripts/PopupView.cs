using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    internal class PopupView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _buttonClosePopup;

        [Header("Settings")]
        [SerializeField] private Vector3 _showSize = Vector3.one;
        [SerializeField] private Vector3 _hideSize = Vector3.zero;
        [SerializeField] private float _duration = 0.3f;


        private void Start() =>
            _buttonClosePopup.onClick.AddListener(Hide);

        private void OnDestroy() =>
            _buttonClosePopup.onClick.RemoveAllListeners();


        public void Show()
        {
            gameObject.SetActive(true);
            transform.DOScale(_showSize, _duration);
        }

        public void ShowDiscrete()
        {
            gameObject.SetActive(true);
            transform.localScale = _showSize;
        }

        public void Hide()
        {
            transform.DOScale(_hideSize, _duration)
                .OnComplete(() => gameObject.SetActive(false)); // post-action
        }

        public void HideDiscrete()
        {
            transform.localScale = _hideSize;
            gameObject.SetActive(false);
        }
    }
}