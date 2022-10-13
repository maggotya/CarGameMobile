using DG.Tweening;
using UnityEngine;

namespace Tween.Examples
{
    internal class TweenCubeSequence : MonoBehaviour
    {
        [SerializeField] private float _duration;

        [Header("Moving")]
        [SerializeField] private int _countMoveLoops;
        [SerializeField] private Ease _moveEase;
        [SerializeField] private Vector3 _endPosition;

        [Header("Scaling")]
        [SerializeField] private float _scaleDelay;
        [SerializeField] private Vector3 _endScale;

        [Header("Jumping")]
        [SerializeField] private float _jumpDelay;
        [SerializeField] private Vector3 _jumpPosition;
        [SerializeField] private float _jumpPower;
        [SerializeField] private int _jumpCount;


        [ContextMenu(nameof(Play))]
        public void Play()
        {
            Sequence sequence = DOTween.Sequence();

            var moveTween = transform.DOMove(_endPosition, _duration).SetLoops(_countMoveLoops).SetEase(_moveEase);
            var scaleTween = transform.DOScale(_endScale, _duration);
            var jumpTween = transform.DOJump(_jumpPosition, _jumpPower, _jumpCount, _duration);

            sequence.Append(moveTween); // добавляем следующим (т.е. первым, т.к. ранее ничего не добавляли)
            sequence.Insert(_scaleDelay, scaleTween); // добавляем через scaleDelay после начала
            sequence.Insert(_jumpDelay, jumpTween); // добавляем через jumpDelay после начала
        }
    }
}