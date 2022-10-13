using UnityEngine;
using System.Collections;

namespace Tween.Examples
{
    internal class MathfLerpComponent : MonoBehaviour
    {
        private const float MinRangeValue = 0;
        private const float MaxRangeValue = 100;
        private const float MinDuration = 0;

        [SerializeField, Range(MinRangeValue, MaxRangeValue)] private float _range = MinRangeValue;
        [SerializeField, Min(MinDuration)] private float _duration = MinDuration;

        private Coroutine _coroutine;


        [ContextMenu(nameof(Play))]
        public void Play()
        {
            Stop();
            _coroutine = StartCoroutine(Playing());
        }

        [ContextMenu(nameof(Stop))]
        public void Stop()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        [ContextMenu(nameof(Reset))]
        public void Reset()
        {
            Stop();
            _range = MinRangeValue;
        }


        private IEnumerator Playing()
        {
            for (float t = 0; t < _duration; t += Time.deltaTime)
            {
                float ratio = CalcRatio(t);
                _range = Mathf.Lerp(MinRangeValue, MaxRangeValue, ratio);
                yield return null;
            }

            _range = MaxRangeValue;
        }

        private float CalcRatio(float time) =>
            _duration switch
            {
                > 0 => time / _duration,
                _ => 0
            };
    }
}