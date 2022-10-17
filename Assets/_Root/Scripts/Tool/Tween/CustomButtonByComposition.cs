using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tool.Tween
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByComposition : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;


        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();
        private void InitComponents()
        {
            _button ??= GetComponent<Button>();
            _audioSource ??= GetComponent<AudioSource>();
            _rectTransform ??= GetComponent<RectTransform>();
        }

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();


        private void OnButtonClick()
        {
            ActivateSound();
            ActivateAnimation();
        }

        private void ActivateSound()
        {
            _audioSource.Play();
        }

        private void ActivateAnimation()
        {
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase);
                    break;

                case AnimationButtonType.ChangePosition:
                    _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase);
                    break;
            }
        }
    }
}