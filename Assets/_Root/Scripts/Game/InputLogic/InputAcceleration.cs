using UnityEngine;
using JoostenProductions;

namespace Game.InputLogic
{
    internal class InputAcceleration : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 0.2f;


        protected override void Move()
        {
            float offset = Mathf.Clamp(Input.acceleration.x, -1, 1);
            float moveValue = Speed * _inputMultiplier * Time.deltaTime * offset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else
                OnLeftMove(abs);
        }
    }
}