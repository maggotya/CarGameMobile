using System;
using System.Collections;
using UnityEngine;

namespace Tween.Examples
{
    internal enum RatioConversion
    {
        Linear,
        Quad,
        Root
    }

    internal class VectorLerpComponent : MonoBehaviour
    {
        [SerializeField] private RatioConversion _ratioConversion;
        [SerializeField, Min(0)] private float _duration;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        private Coroutine _coroutine;


        public void Play(Vector3 startPosition, Vector3 endPosition, float duration)
        {
            Stop();
            _coroutine = StartCoroutine(Playing(startPosition, endPosition, duration));
        }

        [ContextMenu(nameof(Play))]
        public void Play()
        {
            Vector3 startPosition = _startPoint.position;
            Vector3 endPosition = _endPoint.position;

            Play(startPosition, endPosition, _duration);
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
            transform.position = _startPoint.position;
        }


        private IEnumerator Playing(Vector3 startPosition, Vector3 endPosition, float duration)
        {
            Transform transform = this.transform;

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float ratio = CalcRatio(t, duration);
                transform.position = Vector3.Lerp(startPosition, endPosition, ratio);
                yield return null;
            }

            transform.position = endPosition;
        }

        private float CalcRatio(float time, float duration) =>
            duration switch
            {
                > 0 => ConvertRatio(time / duration, _ratioConversion),
                _ => 0
            };

        private float ConvertRatio(float ratio, RatioConversion conversion) =>
            conversion switch
            {
                RatioConversion.Linear => ratio,
                RatioConversion.Quad => Mathf.Pow(ratio, 2),
                RatioConversion.Root => Mathf.Sqrt(ratio),
                _ => throw new ArgumentOutOfRangeException(nameof(RatioConversion))
            };
    }
}