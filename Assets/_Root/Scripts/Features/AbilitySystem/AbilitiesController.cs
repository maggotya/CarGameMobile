using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesController
    {

    }

    internal class AbilitiesController : BaseController, IAbilitiesController
    {
        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _activator;


        public AbilitiesController(
            [NotNull] IAbilitiesView view,
            [NotNull] IAbilitiesRepository repository,
            [NotNull] IEnumerable<IAbilityItem> items,
            [NotNull] IAbilityActivator activator)
        {
            _view
                = view ?? throw new ArgumentNullException(nameof(view));

            _repository
                = repository ?? throw new ArgumentNullException(nameof(repository));

            _activator
                = activator ?? throw new ArgumentNullException(nameof(activator));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            _view.Display(items, OnAbilityViewClicked);
        }

        protected override void OnDispose() =>
            _view.Clear();


        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_activator);
        }
    }
}