using System;
using UnityEngine;

namespace Profile
{
    [CreateAssetMenu(fileName = nameof(InitialProfilePlayer), menuName = "Configs/" + nameof(InitialProfilePlayer))]
    internal class InitialProfilePlayer : ScriptableObject
    {
        [field: SerializeField] public GameState State { get; private set; }
        [field: SerializeField] public InitialProfileCar Car { get; private set; }
    }

    [Serializable]
    internal class InitialProfileCar
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
    }
}
