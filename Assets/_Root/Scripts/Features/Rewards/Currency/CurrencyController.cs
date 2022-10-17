using Tool;
using UnityEngine;

namespace Features.Rewards.Currency
{
    internal class CurrencyController : BaseController
    {
        private readonly ResourcePath _resourcePath = new("Prefabs/Rewards/CurrencyView");
        private readonly CurrencyModel _model;
        private readonly CurrencyView _view;


        public CurrencyController(CurrencyModel currencyModel, Transform placeForUi)
        {
            _model = currencyModel;

            _view = LoadView(placeForUi);
            _view.Init(_model.Wood, _model.Diamond);

            Subscribe(_model);
        }

        protected override void OnDispose()
        {
            Unsubscribe(_model);
            base.OnDispose();
        }


        private CurrencyView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CurrencyView>();
        }


        private void Subscribe(CurrencyModel model)
        {
            model.WoodChanged += OnWoodChanged;
            model.DiamondChanged += OnDiamondChanged;
        }

        private void Unsubscribe(CurrencyModel model)
        {
            model.WoodChanged -= OnWoodChanged;
            model.DiamondChanged -= OnDiamondChanged;
        }

        private void OnWoodChanged() => _view.SetWood(_model.Wood);
        private void OnDiamondChanged() => _view.SetDiamond(_model.Diamond);
    }
}