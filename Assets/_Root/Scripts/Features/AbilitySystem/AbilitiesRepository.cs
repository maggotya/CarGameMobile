using System.Collections.Generic;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }

    internal class AbilitiesRepository : BaseRepository<string, IAbility, IAbilityItem>, IAbilitiesRepository
    {
        public AbilitiesRepository(IEnumerable<IAbilityItem> abilityItems) : base(abilityItems)
        { }

        protected override string GetKey(IAbilityItem abilityItem) => abilityItem.Id;

        protected override IAbility CreateItem(IAbilityItem abilityItem) =>
            abilityItem.Type switch
            {
                AbilityType.Gun => new GunAbility(abilityItem),
                AbilityType.Jump => new JumpAbility(abilityItem),
                _ => StubAbility.Default
            };
    }
}
