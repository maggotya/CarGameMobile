using Tool;
using System;
using UnityEngine;
using JetBrains.Annotations;
using Features.Inventory.Items;

using Object = UnityEngine.Object;

namespace Features.Inventory
{
    internal class InventoryContext : BaseController
    {
        private static readonly ResourcePath _viewPath = new("Prefabs/Inventory/InventoryView");
        private static readonly ResourcePath _dataSourcePath = new("Configs/Inventory/ItemConfigDataSource");


        public InventoryContext([NotNull] Transform placeForUi, [NotNull] IInventoryModel model)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            CreateController(placeForUi, model);
        }


        private InventoryController CreateController(Transform placeForUi, IInventoryModel model)
        {
            InventoryView view = LoadView(placeForUi);
            ItemsRepository repository = CreateRepository();

            InventoryController inventoryController = new(view, model, repository);
            AddController(inventoryController);

            return inventoryController;
        }

        private InventoryView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private ItemConfig[] LoadConfigs() =>
            ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);

        private ItemsRepository CreateRepository()
        {
            ItemConfig[] itemConfigs = LoadConfigs();
            ItemsRepository repository = new(itemConfigs);
            AddRepository(repository);

            return repository;
        }
    }
}