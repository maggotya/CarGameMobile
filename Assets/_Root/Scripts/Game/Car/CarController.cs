using Tool;
using UnityEngine;
using Features.AbilitySystem;

namespace Game.Car
{
    internal class CarController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Game/Car");
        private readonly CarView _view;

        public GameObject ViewGameObject => _view.gameObject;


        public CarController()
        {
            _view = LoadView();
        }


        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}