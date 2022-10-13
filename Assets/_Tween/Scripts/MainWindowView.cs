using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    internal class MainWindowView : MonoBehaviour
    {
        [Header("Popup")]
        [SerializeField] private Button _buttonOpenPopup;
        [SerializeField] private PopupView _popupView;

        [Header("Change Text")]
        [SerializeField] private Button _buttonChangeText;
        [SerializeField] private Text _changeableText;
        [SerializeField] private string _newText;
        [SerializeField] private Ease _textEaseType;
        [SerializeField] private float _textDuration;


        private void Start()
        {
            _buttonOpenPopup.onClick.AddListener(_popupView.Show);
            _buttonChangeText.onClick.AddListener(ChangeText);

            _popupView.HideDiscrete();
        }

        private void OnDestroy()
        {
            _buttonOpenPopup.onClick.RemoveAllListeners();
            _buttonChangeText.onClick.RemoveAllListeners();
        }


        private void ChangeText() =>
            _changeableText.DOText(_newText, _textDuration).SetEase(_textEaseType);
    }
}