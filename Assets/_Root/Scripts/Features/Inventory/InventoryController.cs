using Tool;
using System;
using UnityEngine;
using JetBrains.Annotations;
using Features.Inventory.Items;
using Object = UnityEngine.Object;

namespace Features.Inventory
{
    internal interface IInventoryController
    {
    }

    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Inventory/InventoryView");
        private readonly ResourcePath _dataSourcePath = new("Configs/Inventory/ItemConfigDataSource");

        private readonly InventoryView _view;
        private readonly IInventoryModel _model;
        private readonly ItemsRepository _repository;


        public InventoryController(
            [NotNull] Transform placeForUi,
            [NotNull] IInventoryModel inventoryModel)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _model
                = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));

            _repository = CreateRepository();
            _view = LoadView(placeForUi);

            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemId in _model.EquippedItems)
                _view.Select(itemId);
        }


        private ItemsRepository CreateRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            ItemsRepository repository = new(itemConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }


        private void OnItemClicked(string itemId)
        {
            bool equipped = _model.IsEquipped(itemId);

            if (equipped)
                UnequipItem(itemId);
            else
                EquipItem(itemId);
        }

        private void EquipItem(string itemId)
        {
            _view.Select(itemId);
            _model.EquipItem(itemId);
        }

        private void UnequipItem(string itemId)
        {
            _view.Unselect(itemId);
            _model.UnequipItem(itemId);
        }
    }
}