using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tool.Tween
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButton_Obsolete : Button
    {
        public static string AnimationTypeName => nameof(_animationButtonType);
        public static string CurveEaseName => nameof(_curveEase);
        public static string DurationName => nameof(_duration);

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;

        private Tweener _tweenAnimation;


        protected override void Awake()
        {
            base.Awake();
            InitComponents();
        }

        protected new void OnValidate() =>
            InitComponents();

        private void InitComponents()
        {
            _audioSource ??= GetComponent<AudioSource>();
            _rectTransform ??= GetComponent<RectTransform>();
        }


        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ActivateAnimation();
            ActivateSound();
        }


        [ContextMenu(nameof(ActivateAnimation))]
        private void ActivateAnimation()
        {
            StopAnimation();

            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _tweenAnimation = _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase);
                    break;

                case AnimationButtonType.ChangePosition:
                    _tweenAnimation = _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase);
                    break;
            }
        }

        [ContextMenu(nameof(ActivateSound))]
        private void ActivateSound()
        {
            _audioSource.Play();
        }

        [ContextMenu(nameof(StopAnimation))]
        private void StopAnimation() =>
            _tweenAnimation?.Kill();
    }
}