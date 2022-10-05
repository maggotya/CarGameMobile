using UnityEngine;

namespace Features.AbilitySystem
{
    internal interface IAbilityActivator
    {
        float JumpHeight { get; }
        GameObject ViewGameObject { get; }
    }
}
