using Tool;
using UnityEngine;

namespace Features.Rewards.Currency
{
    internal class CurrencyController : BaseController
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);

        private readonly ResourcePath _resourcePath = new("Prefabs/Rewards/CurrencyView");
        private readonly CurrencyView _view;

        private int Wood
        {
            get => PlayerPrefs.GetInt(WoodKey, 0);
            set => PlayerPrefs.SetInt(WoodKey, value);
        }

        private int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey, 0);
            set => PlayerPrefs.SetInt(DiamondKey, value);
        }


        public CurrencyController(Transform placeForUi)
        {
            _view = LoadView(placeForUi);
            _view.Init(Wood, Diamond);
        }

        private CurrencyView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CurrencyView>();
        }


        public void AddWood(int value)
        {
            Wood += value;
            _view.SetWood(Wood);
        }

        public void AddDiamond(int value)
        {
            Diamond += value;
            _view.SetDiamond(Diamond);
        }
    }
}