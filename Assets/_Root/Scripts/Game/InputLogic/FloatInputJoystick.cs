using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal class FloatInputJoystick : BaseInputView, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Header("Components")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private CanvasGroup _container;

        [Header("Settings")]
        [SerializeField] private float _enabledAlpha = 1;
        [SerializeField] private float _disabledAlpha = 0;
        [SerializeField] private float _inputMultiplier = 10;

        private bool _usingJoystick;


        public void OnPointerDown(PointerEventData eventData)
        {
            _joystick.transform.position = eventData.position;
            _joystick.SetStartPosition(eventData.position);
            _joystick.OnPointerDown(eventData);
            StartUsing();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystick.OnPointerUp(eventData);
            FinishUsing();
        }

        public void OnDrag(PointerEventData eventData) =>
            _joystick.OnDrag(eventData);


        private void StartUsing()
        {
            _usingJoystick = true;
            SetActive(true);
        }

        private void FinishUsing()
        {
            _usingJoystick = false;
            SetActive(false);
        }

        private void SetActive(bool active) =>
            _container.alpha = active ? _enabledAlpha : _disabledAlpha;


        protected override void Move()
        {
            if (!_usingJoystick)
                return;

            float axisOffset = CrossPlatformInputManager.GetAxis("Horizontal");
            float moveValue = Speed * _inputMultiplier * Time.deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else
                OnLeftMove(abs);
        }
    }
}