using System;
using UnityEngine;
using JetBrains.Annotations;
using JoostenProductions;

namespace Features.AbilitySystem.Abilities
{
    internal class JumpAbility : IAbility
    {
        private const float StartTime = 0f;

        private readonly IAbilityItem _abilityItem;

        private float _time;
        private bool _isActive;
        private Transform _transformCache;
        private float _startHeight;
        private float _targetHeight;
        private float _jumpDuration;

        private enum JumpPhase { Up, Down }
        private JumpPhase _jumpPhase;


        public JumpAbility([NotNull] IAbilityItem abilityItem) =>
            _abilityItem = abilityItem ?? throw new ArgumentNullException(nameof(abilityItem));


        public void Apply(IAbilityActivator activator)
        {
            if (_isActive)
                return;

            StartAbility(activator);
        }

        private void StartAbility(IAbilityActivator activator)
        {
            _isActive = true;
            _time = StartTime;
            _transformCache = activator.ViewGameObject.transform;
            _startHeight = _transformCache.position.y;
            _targetHeight = _startHeight + activator.JumpHeight;
            _jumpDuration = _abilityItem.Value;
            _jumpPhase = JumpPhase.Up;

            UpdateManager.SubscribeToUpdate(Update);
        }

        private void FinishAbility()
        {
            _isActive = false;
            _time = default;
            _transformCache = default;
            _startHeight = default;
            _targetHeight = default;
            _jumpDuration = default;
            _jumpPhase = default;

            UpdateManager.UnsubscribeFromUpdate(Update);
        }

        private void Update()
        {
            UpdateTime();
            UpdatePosition();
            UpdateState();
        }


        private void UpdateTime()
        {
            switch (_jumpPhase)
            {
                case JumpPhase.Up: IncreaseTime();
                    break;

                case JumpPhase.Down: DecreaseTime();
                    break;
            }
        }

        private void IncreaseTime() =>
            _time += Time.deltaTime;

        private void DecreaseTime() =>
            _time -= Time.deltaTime;


        private void UpdatePosition()
        {
            float currentHeight = CalcCurrentHeight();
            _transformCache.position = CalcCurrentPosition(currentHeight);
        }

        private float CalcCurrentHeight()
        {
            float ratio = _time / _jumpDuration;
            return Mathf.Lerp(_startHeight, _targetHeight, ratio);
        }

        private Vector3 CalcCurrentPosition(float height)
        {
            Vector3 position = _transformCache.position;
            position.y = height;
            return position;
        }


        private void UpdateState()
        {
            if (IsJumpPeak())
                _jumpPhase = JumpPhase.Down;

            if (IsJumpFinished())
                FinishAbility();
        }

        private bool IsJumpPeak() =>
            _jumpPhase == JumpPhase.Up &&
            _time >= _jumpDuration;

        private bool IsJumpFinished() =>
            _jumpPhase == JumpPhase.Down &&
            _time <= StartTime;
    }
}
