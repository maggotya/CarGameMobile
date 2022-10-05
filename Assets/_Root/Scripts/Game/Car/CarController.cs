using Tool;
using UnityEngine;
using Features.AbilitySystem;

namespace Game.Car
{
    internal class CarController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Game/Car");
        private readonly CarModel _model;
        private readonly CarView _view;

        public float JumpHeight => _model.JumpHeight;
        public GameObject ViewGameObject => _view.gameObject;


        public CarController(CarModel model)
        {
            _model = model;
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