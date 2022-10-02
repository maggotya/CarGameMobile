using Tool;
using Profile;
using Services;
using UnityEngine;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using System.Collections.Generic;
using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly IAbilitiesController _abilitiesController;
        private readonly TapeBackgroundController _tapeBackgroundController;


        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _carController = CreateCarController();
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesController = CreateAbilitiesController(placeForUi, _carController);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);

            ServiceRoster.Analytics.SendGameStarted();
        }


        private TapeBackgroundController CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            TapeBackgroundController tapeBackgroundController = new(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController(ProfilePlayer profilePlayer,
            SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            InputGameController inputGameController = new(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }

        private CarController CreateCarController()
        {
            CarController carController = new();
            AddController(carController);

            return carController;
        }

        private IAbilitiesController CreateAbilitiesController(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            AbilityItemConfig[] itemConfigs = LoadAbilityItemConfigs();
            AbilitiesRepository repository = CreateAbilitiesRepository(itemConfigs);
            AbilitiesView view = LoadAbilitiesView(placeForUi);

            AbilitiesController controller = new(view, repository, itemConfigs, abilityActivator);
            AddController(controller);

            return controller;
        }

        private AbilityItemConfig[] LoadAbilityItemConfigs()
        {
            ResourcePath path = new("Configs/Ability/AbilityItemConfigDataSource");
            return ContentDataSourceLoader.LoadAbilityItemConfigs(path);
        }

        private AbilitiesRepository CreateAbilitiesRepository(IEnumerable<IAbilityItem> abilityItemConfigs)
        {
            AbilitiesRepository repository = new(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }

        private AbilitiesView LoadAbilitiesView(Transform placeForUi)
        {
            var path = new ResourcePath("Prefabs/Ability/AbilitiesView");

            GameObject prefab = ResourcesLoader.LoadPrefab(path);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
}