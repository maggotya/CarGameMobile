using Tool;
using System;
using UnityEngine;
using JetBrains.Annotations;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesController
    { }

    internal class AbilitiesController : BaseController
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _dataSourcePath = new("Configs/Ability/AbilityItemConfigDataSource");

        private readonly AbilitiesView _view;
        private readonly AbilitiesRepository _repository;
        private readonly IAbilityActivator _abilityActivator;


        public AbilitiesController(
            [NotNull] Transform placeForUi,
            [NotNull] IAbilityActivator abilityActivator)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _abilityActivator
                = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));

            AbilityItemConfig[] abilityItemConfigs = LoadAbilityItemConfigs();
            _repository = CreateRepository(abilityItemConfigs);
            _view = LoadView(placeForUi);

            _view.Display(abilityItemConfigs, OnAbilityViewClicked);
        }


        private AbilityItemConfig[] LoadAbilityItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourcePath);

        private AbilitiesRepository CreateRepository(AbilityItemConfig[] abilityItemConfigs)
        {
            AbilitiesRepository repository = new(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }

        private AbilitiesView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }


        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}