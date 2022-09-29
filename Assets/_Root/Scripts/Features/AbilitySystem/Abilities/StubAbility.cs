namespace Features.AbilitySystem.Abilities
{
    internal class StubAbility : IAbility
    {
        public static readonly IAbility Default = new StubAbility();

        public void Apply(IAbilityActivator activator)
        { }
    }
}
