using DG.Tweening;
using UnityEngine;

namespace Tween.Examples
{
    [RequireComponent(typeof(Renderer))]
    internal class TweenCubeColor : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Renderer _renderer;

        [Header("Settings")]
        [SerializeField] private float _duration;
        [SerializeField] private Color _endColor;


        private void Awake() => InitRenderer();
        private void OnValidate() => InitRenderer();
        private void InitRenderer() => _renderer ??= GetComponent<Renderer>();


        [ContextMenu(nameof(Play))]
        public void Play()
        {
            Material material = _renderer.material;
            material.DOColor(_endColor, _duration);
        }
    }
}
