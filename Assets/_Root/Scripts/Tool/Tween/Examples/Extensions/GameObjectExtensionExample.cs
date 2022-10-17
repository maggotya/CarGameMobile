using UnityEngine;

namespace Tool.Tween.Examples.Extensions
{
    internal class GameObjectExtensionExample : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameObject _gameObject;

        [Header("Settings")]
        [SerializeField] private float _duration;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;


        [ContextMenu(nameof(Play))]
        public void Play() =>
            _gameObject.Move(_startPoint.position, _endPoint.position, _duration);
    }
}
