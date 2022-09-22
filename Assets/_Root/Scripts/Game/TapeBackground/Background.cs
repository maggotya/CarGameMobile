using UnityEngine;

namespace Game.TapeBackground
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal class Background : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _relativeSpeedRate;

        private Vector2 _size;
        private Vector3 _cachedPosition;

        private float LeftBorder => _cachedPosition.x - _size.x / 2;
        private float RightBorder => _cachedPosition.x + _size.x / 2;


        private void Awake()
        {
            _cachedPosition = transform.position;
            _size = _spriteRenderer.size;
        }

        private void OnValidate() =>
            _spriteRenderer ??= GetComponent<SpriteRenderer>();


        public void Move(float value)
        {
            Vector3 position = transform.position;
            position += Vector3.right * value * _relativeSpeedRate;

            if (position.x <= LeftBorder)
                position.x = RightBorder - (LeftBorder - position.x);

            if (position.x >= RightBorder)
                position.x = LeftBorder + (RightBorder - position.x);

            transform.position = position;
        }
    }
}
