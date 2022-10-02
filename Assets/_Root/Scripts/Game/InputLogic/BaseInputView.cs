using Tool;
using UnityEngine;
using JoostenProductions;

namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        protected float Speed;

        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            float speed)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            Speed = speed;
        }


        protected abstract void Move();

        protected void OnLeftMove(float value) =>
            _leftMove.Value = value;

        protected void OnRightMove(float value) =>
            _rightMove.Value = value;
    }
}
