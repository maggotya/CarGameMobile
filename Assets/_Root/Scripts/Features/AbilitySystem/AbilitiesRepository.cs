using System.Collections.Generic;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }

    internal class AbilitiesRepository : BaseRepository<string, IAbility, AbilityItemConfig>
    {
        public AbilitiesRepository(IEnumerable<AbilityItemConfig> configs) : base(configs)
        { }

        protected override string GetKey(AbilityItemConfig config) => config.Id;

        protected override IAbility CreateItem(AbilityItemConfig config) =>
            config.Type switch
            {
                AbilityType.Gun => new GunAbility(config),
                _ => StubAbility.Default
            };
    }
}
